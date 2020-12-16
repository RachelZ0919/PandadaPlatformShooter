using UnityEngine;
using GameLogic.EntityStats;

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

        private Stats stat;
        private float hitStartTime;
        private Rigidbody2D rigidbody;
        private Animator animator;
        

        private void Awake()
        {
            stat = GetComponent<Stats>();
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
        }

        //todo:异常状态
        public void GetHit(float damage, float knockbackForce, Vector2 direction) {

            if (Time.time - hitStartTime >= indivisibleDuration)
            {
                stat.SetValue("health", stat.health - damage);

                //击退
                if (canGetKnockbacked)
                {
                    float knockBack = Mathf.Max(0, knockbackForce - stat.knockBackResist) + 0.5f;
                    rigidbody.AddForce(direction * knockBack);
                }

                //视觉
                animator.SetBool("isHit", true);

                //无敌状态
                hitStartTime = Time.time;
            }
        }

        private void LateUpdate()
        {
            animator.SetBool("isHit", false);
        }

    }
}