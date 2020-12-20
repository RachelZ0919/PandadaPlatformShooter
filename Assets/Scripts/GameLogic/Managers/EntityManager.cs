using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using GameLogic.Item.Weapon;

namespace GameLogic.Managers
{
    [RequireComponent(typeof(Stats),typeof(Animator),typeof(Rigidbody2D))]
    /// <summary>
    /// 负责管理玩家属性
    /// </summary>
    public class EntityManager : MonoBehaviour
    {
        [SerializeField] private StatData statData;
        [SerializeField] private AudioManager usingAudio;
        
        private Animator animator;
        ShootingBehavior shootingBehavior;
        MovingBehavior movingBehavior;
        HitBehavior hitBehavior;
        Collider2D collider;
        Rigidbody2D rigidbody;
        Stats stat;

        public delegate void OnDeath(EntityManager entity);

        public OnDeath OnObjectDeath;
        public OnDeath OnObjectVisuallyDeath;
        private bool hasDead;

        private void Awake()
        {
            Debug.Log("Entity -" + name);
            //加载组件
            animator = GetComponent<Animator>();
            shootingBehavior = GetComponent<ShootingBehavior>();
            movingBehavior = GetComponent<MovingBehavior>();
            hitBehavior = GetComponent<HitBehavior>();
            collider = GetComponent<Collider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
            stat = GetComponent<Stats>();

            //音效初始化
            if (shootingBehavior != null)
            {
                shootingBehavior.audio = usingAudio;
            }

            if (movingBehavior != null)
            {
                movingBehavior.audio = usingAudio;
            }

            if (hitBehavior != null)
            {
                hitBehavior.audio = usingAudio;
            }
        }

        private void Start()
        {
            Initialize();
            Debug.Log("start" + name);
        }


        public void Initialize()
        {
            //初始化角色属性
            stat.InitializeStats(statData);
            stat.OnStatsChanged += OnStatChange;

            //人物死亡属性相关
            hasDead = false;
            GameManager.instance.OnObjectCreate(this);

            //装枪
            if(shootingBehavior != null)
            {
                if (statData.defaultWeapon != null)
                {
                    Weapon weapon = Instantiate(statData.defaultWeapon).GetComponent<Weapon>();
                    weapon.PickUp(transform);
                }
            }

            //动画初始化
            animator.SetBool("isDead", false);
        }

        /// <summary>
        /// 属性变化时回调函数，检测是否死亡并通知游戏管理
        /// </summary>
        /// <param name="stat">属性</param>
        private void OnStatChange(Stats stat)
        {
            if (!hasDead && stat.health <= 0)
            {
                hasDead = true;

                //删除死亡检测回调
                stat.OnStatsChanged -= OnStatChange;

                //开始死亡动画
                animator.SetBool("isDead", true);

                //死亡通知
                OnObjectVisuallyDeath?.Invoke(this);

            }
        }

        /// <summary>
        /// 死亡动画结束后回调函数
        /// </summary>
        public void OnDeadAnimationEnd()
        {
            //通知物体死亡
            OnObjectDeath?.Invoke(this);
            gameObject.SetActive(false);
        }

    }
}