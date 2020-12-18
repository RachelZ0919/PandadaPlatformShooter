using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using GameLogic.Item.Weapon;

namespace GameLogic.Managers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Stats),typeof(Animator),typeof(SpriteRenderer))]
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

        public delegate void OnDeath(GameObject obj);

        public OnDeath OnObjectDeath;
        private bool hasDead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            shootingBehavior = GetComponent<ShootingBehavior>();
            movingBehavior = GetComponent<MovingBehavior>();
            hitBehavior = GetComponent<HitBehavior>();
            collider = GetComponent<Collider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            //初始化角色属性
            Stats stat = GetComponent<Stats>();
            stat.InitializeStats(statData);
            stat.OnStatsChanged += OnStatChange;

            hasDead = false;

            //初始化枪
            if(shootingBehavior != null)
            {
                if(statData.defaultWeapon != null)
                {
                    Weapon weapon = Instantiate(statData.defaultWeapon).GetComponent<Weapon>();
                    weapon.PickUp(transform);
                }
            }

            if(movingBehavior != null)
            {
                movingBehavior.audio = usingAudio;
            }

            if (hitBehavior != null)
            {
                hitBehavior.audio = usingAudio;
            }

            GameManager.instance.OnObjectCreate(this);
        }

        private void OnStatChange(Stats stat)
        {
            if (!hasDead && stat.health <= 0)
            {
                Debug.Log(name + " is Dead");
                hasDead = true;

                //关闭所有behavior和碰撞体
                if (shootingBehavior != null) shootingBehavior.enabled = false;
                if (movingBehavior != null) movingBehavior.enabled = false;
                if (hitBehavior != null) hitBehavior.enabled = false;
                if (collider != null) collider.enabled = false;
                rigidbody.simulated = false;

                //开始死亡动画
                animator.SetBool("isDead", true);
                
                //通知物体死亡
                OnObjectDeath(gameObject);
            }
        }

        public void OnDeadAnimationEnd()
        {
            gameObject.SetActive(false);
        }
    }
}