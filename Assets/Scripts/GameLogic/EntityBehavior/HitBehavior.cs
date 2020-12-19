using UnityEngine;
using GameLogic.EntityStats;
using GameLogic.Managers;
using CameraLogic;

namespace GameLogic.EntityBehavior
{
    public class HitBehavior : MonoBehaviour
    {
        /// <summary>
        /// 击中后无敌状态时间
        /// </summary>
        public float indivisibleDuration = 0.5f;
        /// <summary>
        /// 是否受击退效果
        /// </summary>
        public bool canGetKnockbacked = false;
        /// <summary>
        /// 击中后是否开启震屏
        /// </summary>
        public bool enableScreenShake = false;

        private Stats stat;
        private float hitStartTime;
        private Rigidbody2D rigidbody;
        private Animator animator;

        private Vector3 recoverOffset;
        private Vector3 currentOffset;
        private Vector3 lastOffset;
        private Vector3 recoverVel;

        [HideInInspector]public AudioManager audio;
        

        private void Awake()
        {
            stat = GetComponent<Stats>();
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            hitStartTime = Time.time - indivisibleDuration;
        }

        private void Update()
        {
            if (Vector3.Distance(recoverOffset, currentOffset) > 0.01f) 
            {
                currentOffset = Vector3.SmoothDamp(currentOffset, recoverOffset, ref recoverVel, 0.1f);
                transform.position += currentOffset - lastOffset;
                lastOffset = currentOffset;
            }

            animator.SetFloat("indivisibleTime", indivisibleDuration - Time.time + hitStartTime);
        }

        //todo:异常状态
        /// <summary>
        /// 实体受到伤害
        /// </summary>
        /// <param name="damage">伤害量</param>
        /// <param name="knockbackForce">击退强度</param>
        /// <param name="direction">击退方向</param>
        public void GetHit(float damage, float knockbackForce, Vector2 direction) 
        {
            if (Time.time - hitStartTime >= indivisibleDuration)
            {
                stat.SetValue("health", stat.health - damage);

                //击退
                if (canGetKnockbacked)
                {
                    float knockBack = (Mathf.Max(0, knockbackForce - stat.knockBackResist) + 0.2f) * 0.1f;
                    recoverOffset = direction.normalized * Mathf.Min(knockBack, 0.5f);
                    Vector3 positionOffset = direction.normalized * knockBack * 1.5f;
                    transform.position -= positionOffset;
                    lastOffset = currentOffset = Vector3.zero;
                }

                //视觉
                animator.SetBool("isHit", true);

                //无敌状态
                hitStartTime = Time.time;

                //音效
                if(audio != null) audio.PlayAudio("hitAudio");

                if (enableScreenShake)
                {
                    CameraShake.instance.ShakeScreen(0.3f, 0.02f);
                }
            }
        }

        private void LateUpdate()
        {
            animator.SetBool("isHit", false);
            if (Vector3.Distance(recoverOffset, currentOffset) > 0.01f)
            {
                rigidbody.velocity *= 0.2f;
            }
        }

    }
}