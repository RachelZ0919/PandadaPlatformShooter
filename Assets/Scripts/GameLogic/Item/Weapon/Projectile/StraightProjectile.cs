﻿using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using VisualEffect;

namespace GameLogic.Item.Weapon
{
    /// <summary>
    /// 射程看作距离的直线子弹
    /// </summary>
    public class StraightProjectile : Projectile
    {
        private Vector2 startPosition = Vector2.zero;
        private bool isLaunched = false;

        private void Update()
        {
            //超过射程回收子弹
            if(isLaunched && Vector2.Distance(startPosition,rigidbody.position) >= range)
            {
                //todo:消失时动画
                ProjectilePool.instance.RecycleProjectile(poolName, this);
            }
        }

        public override void Initialize()
        {
            collider.enabled = true;
            rigidbody.velocity = Vector2.zero;
            isLaunched = true;
        }

        public override void Launch(Vector2 position, Vector2 direction)
        {
            startPosition = transform.position = position;
            rigidbody.velocity = direction.normalized * speed;

            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, angle);
            isLaunched = true;
        }

        protected override void OnHit(GameObject hitObject, Vector3 hitPos, Vector3 hitDirection)
        {
            HitBehavior hit = hitObject.GetComponent<HitBehavior>();
            Stats stat = hitObject.GetComponent<Stats>();
            if (hit != null && stat != null) 
            {
                damage.DealDamage(hit, stat, hitDirection.normalized);
            }
            //todo:击中效果优化，击中玩家没有效果
            EffectPool.instance.PlayEffect("hit_effect", hitPos, hitDirection);
            OnDead();
        }
    }
}