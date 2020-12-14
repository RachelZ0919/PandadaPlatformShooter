using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;

namespace GameLogic.Item.Weapon
{
    /// <summary>
    /// 枪种类武器的基类
    /// </summary>
    abstract public class Weapon : MonoBehaviour,IItem
    {
        //todo:支持多种子弹
        [SerializeField] protected WeaponData weaponData; //基本武器数据
        [SerializeField] protected ProjectileData projectileData; //子弹数据
        protected Transform entity; //持有武器的实体
        protected bool isPickedUp = false; //当前是否被拾

        //射击用逻辑
        protected float lastShootingTime; //上次射击时间
        protected int projectileLeft; //子弹数量
        protected bool isReloading; //是否在换弹
        private float reloadStartTime; //换弹开始时间
        protected Vector3 shootingPosition; //子弹生成点

        [SerializeField] private Transform shootingPoint; //

        public void PickUp(Transform entity)
        {
            //可能要提前判断一下entity能不能拾取，也可能不需要，pickingBehavior管这个
            this.entity = entity;
            ShootingBehavior behavior = entity.GetComponent<ShootingBehavior>();
            if(behavior != null)
            {
                behavior.weapon = this; //给射击动作这把武器
            }

            //计算对象池大小
            //todo:同时要考虑基础属性
            float length = Mathf.Min(10, weaponData.range); //前者是场景大小获得的经验值，场景比例改变，就要修改这个。
            float projectileLife = length / weaponData.projectileSpeed; //子弹从发射到消亡的平均时间
            float totalClips = projectileLife / (weaponData.projectilesPerClip /
                                      weaponData.shootingSpeed + weaponData.cooldownTime); //从发射到消亡能发射的子弹数量（以弹夹为单位） = 平均时间 / （一弹夹子弹量 * 子弹发射时间间隔 + 换弹时间）
            int poolSize = Mathf.CeilToInt(totalClips * weaponData.projectilesPerClip + 3); //换算成子弹数量，并且加了一丢丢子弹
            //生成子弹对象
            Projectile projectilePrefab = projectileData.GenerateProjectile();
            //设置子弹碰撞层
            LayerMask layermaskToHit = 1 << 8 | 1 << 9 | 1 << 10;
            layermaskToHit &= ~(1 << gameObject.layer);
            projectilePrefab.layermaskToHit = layermaskToHit;
            //设置子弹所属对象池
            projectilePrefab.poolName = weaponData.weaponName;
            //申请对象池
            ProjectilePool.instance.AddPool(weaponData.weaponName, null, poolSize, false);

            //todo:枪显示

            //枪初始化
            lastShootingTime = Time.time - weaponData.shootingSpeed;
            projectileLeft = weaponData.projectilesPerClip;
            isReloading = false;
        }

        virtual protected void Update()
        {
            //换弹倒数
            if (isReloading)
            {
                //todo：UI显示
                if(Time.time - reloadStartTime >= weaponData.cooldownTime)
                {
                    isReloading = false;
                    //todo:关闭UI
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
        abstract public void Shoot(Vector2 direction , ShootingBaseStats baseStats);
    }
}