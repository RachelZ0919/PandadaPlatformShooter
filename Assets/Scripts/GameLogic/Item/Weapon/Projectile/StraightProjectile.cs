using UnityEngine;
using System.Collections;

namespace GameLogic.Item.Weapon
{
    public class StraightProjectile : Projectile
    {
        Vector2 startPosition = Vector2.zero;

        private void Update()
        {
            //超过射程回收子弹
            if(Vector2.Distance(startPosition,rigidbody.position) >= range)
            {
                //todo:消失时动画
                ProjectilePool.instance.RecycleProjectile(poolName, this);
            }
        }

        public override void Initialize()
        {
            rigidbody.velocity = Vector2.zero;
        }

        public override void Launch(Vector2 position, Vector2 direction)
        {
            transform.position = position;
            rigidbody.velocity = direction * speed;
        }

    }
}