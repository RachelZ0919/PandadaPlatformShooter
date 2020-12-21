using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;
using VisualEffect;

namespace GameLogic.Item.Weapon
{
    [RequireComponent(typeof(SpriteRenderer))]
    /// <summary>
    /// 枪种类武器的逻辑
    /// </summary>
    abstract public class Weapon : MonoBehaviour,IItem
    {
        //todo:支持多种子弹
        [SerializeField] protected WeaponData weaponData; //基本武器数据
        [SerializeField] private Sprite gunSprite; //枪贴图
        [SerializeField] private Sprite itemSprite; //物体
        [SerializeField] private bool destroyOnLoad = false;
        protected Transform entity; //持有武器的实体
        protected bool isPickedUp = false; //当前是否被拾取

        //射击用逻辑
        protected float lastShootingTime; //上次射击时间
        protected int projectileLeft; //子弹数量
        protected bool isReloading; //是否在换弹
        private float reloadStartTime; //换弹开始时间

        //动画
        [SerializeField] private GameObject shootEffect; //武器发射特效 
        protected string effectName;
        protected Animator animator;
        private Vector3 dampVelocity;

        //音效
        public AudioClip shootingAudio
        {
            get
            {
                return weaponData.shootingAudio;
            }
        }

        public void PickUp(Transform entity)
        {
            isPickedUp = true;

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                rigidbody.bodyType = RigidbodyType2D.Static;
                rigidbody.simulated = false;
            }


            //可能要提前判断一下entity能不能拾取，也可能不需要，pickingBehavior管这个
            this.entity = entity;
            ShootingBehavior behavior = entity.GetComponent<ShootingBehavior>();
            if(behavior != null)
            {
                behavior.weapon = this; //给射击动作这把武器
                transform.parent = behavior.holdingPoint;//把武器设置成实体子物体
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = new Vector3(1, 1, 1);
            }

            //初始化枪
            InitializeGun();

            //设置枪的贴图
            GetComponent<SpriteRenderer>().sprite = gunSprite;
        }


        virtual protected void Awake()
        {
            animator = GetComponent<Animator>();
            isReloading = false;
            isPickedUp = false;
        }

        virtual protected void Update()
        {
            if (isPickedUp)
            {
                //换弹倒数
                if (isReloading)
                {
                    //todo：UI显示
                    if (Time.time - reloadStartTime >= weaponData.cooldownTime)
                    {
                        isReloading = false;
                        //todo:关闭UI
                    }
                }

                //射击动画
                //if (animator != null) animator.SetFloat("shootingTime", Time.time - lastShootingTime);


                //后坐力回复
                if (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.01f)
                {
                    transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref dampVelocity, 0.2f);
                }
                else
                {
                    transform.localPosition = Vector3.zero;
                }
            } 
        }

        /// <summary>
        /// 换弹
        /// </summary>
        protected void Reload()
        {
            //重设子弹数量
            projectileLeft = weaponData.projectilesPerClip;
            //todo：显示UI
            //开始计数
            isReloading = true;
            reloadStartTime = Time.time;
        }

        /// <summary>
        /// 朝指定方向射出子弹
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="baseStats">影响最终输出的人物参数</param>
        /// <returns>是否射出子弹</returns>
        abstract public bool Shoot(Vector2 direction, ShootingBaseStats baseStats);

        /// <summary>
        /// 注册对象池
        /// </summary>
        private void RegisterPool()
        {
            //计算对象池大小
            //todo:同时要考虑基础属性
            float length = Mathf.Min(10, weaponData.range); //前者是场景大小获得的经验值，场景比例改变，就要修改这个。
            float projectileLife = length / weaponData.projectileSpeed; //子弹从发射到消亡的平均时间
            float totalClips = projectileLife / (weaponData.projectilesPerClip /
                                      weaponData.shootingSpeed + weaponData.cooldownTime); //从发射到消亡能发射的子弹数量（以弹夹为单位） = 平均时间 / （一弹夹子弹量 * 子弹发射时间间隔 + 换弹时间）
            //int poolSize = Mathf.CeilToInt(totalClips * weaponData.projectilesPerClip + 3); //换算成子弹数量，并且加了一丢丢子弹K
            int poolSize = 5;
            //申请对象池
            ProjectilePool.instance.AddPool(weaponData.projectilePoolName, weaponData.projectile, poolSize, destroyOnLoad);
            if (shootEffect != null)
            {
                EffectPool.instance.AddEffect(shootEffect.name, poolSize, shootEffect);
                effectName = shootEffect.name;
            }
        }

        /// <summary>
        /// 枪初始化
        /// </summary>
        public void InitializeGun()
        {
            //注册子弹池
            RegisterPool();
            //枪初始化
            lastShootingTime = Time.time - weaponData.shootingSpeed;
            projectileLeft = weaponData.projectilesPerClip;
            isReloading = false;
        }

        /// <summary>
        /// 应用后坐力
        /// </summary>
        /// <param name="direction">射击方向</param>
        protected void ApplyRecoilForce()
        {
            Vector2 deltaPosition = Vector2.left * weaponData.recoilForce * 0.5f;
            transform.localPosition = new Vector3(deltaPosition.x, deltaPosition.y, 0);
        }

        protected Projectile GetAProjectile(ShootingBaseStats baseStats)
        {
            //获取子弹
            Projectile projectile = ProjectilePool.instance.SpawnAProjectile(weaponData.projectilePoolName);

            //射子弹
            if(transform.parent.gameObject.layer == 9)
            {
                projectile.layermaskToHit = 1 << 8 | 1 << 10 | 1 << 12 | 1 << 13;
            }
            else
            {
                projectile.layermaskToHit = 1 << 9 | 1 << 8;
            }
            projectile.damage.damage = weaponData.attack + baseStats.baseAttack;
            projectile.damage.knockbackForce = weaponData.knockbackForce;
            projectile.speed = weaponData.projectileSpeed + baseStats.baseProjectileSpeed;
            projectile.range = weaponData.range + baseStats.baseRange;
            projectile.lastTime = weaponData.lastTime;

            return projectile;

        } 

        virtual protected void OnDestroy()
        {
            ProjectilePool.instance.DeletePool(weaponData.projectilePoolName);//删除子弹池
        }

    }
}