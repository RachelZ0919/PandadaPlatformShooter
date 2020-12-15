using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;

namespace GameLogic.Item.Weapon
{
    /// <summary>
    /// 子弹基类
    /// </summary>
    abstract public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// 应该发生碰撞的实体layer
        /// </summary>
        public LayerMask layermaskToHit { get; set; }
        /// <summary>
        /// 子弹击中后理应造成的伤害
        /// </summary>
        public Damage damage { get; set; } //todo:伤害类型可以复杂化，未来在这里修改
        /// <summary>
        /// 子弹速度
        /// </summary>
        public float speed { get; set; }
        /// <summary>
        /// 子弹射程
        /// </summary>
        public float range { get; set; }
        /// <summary>
        /// 所属对象池
        /// </summary>
        public string poolName { protected get; set; }

        protected Rigidbody2D rigidbody;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            //判断是不是在应该撞的layer
            if (((1 << collision.gameObject.layer) & layermaskToHit) > 0)
            {
                OnHit(collision.gameObject);
            }
        }

        /// <summary>
        /// 初始化子弹行为
        /// </summary>
        abstract public void Initialize();

        /// <summary>
        /// 发射子弹，发射时参数设置override这个函数
        /// </summary>
        /// <param name="direction">发射方向</param>
        /// <param name="position">发射起点</param>
        abstract public void Launch(Vector2 position, Vector2 direction);

        /// <summary>
        /// 碰撞时调用，击中后行为override这个函数
        /// </summary>
        /// <param name="hitObject">碰撞物体</param>
        virtual protected void OnHit(GameObject hitObject)
        {
            Stats stat = hitObject.GetComponent<Stats>();
            if(stat != null)
            {
                damage.DealDamage(stat);
            }
            ProjectilePool.instance.RecycleProjectile(poolName, this); //回收
        }
    }
}