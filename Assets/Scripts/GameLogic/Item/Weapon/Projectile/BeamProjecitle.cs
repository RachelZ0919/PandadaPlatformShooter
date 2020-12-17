using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;
using GameLogic.EntityStats;

namespace GameLogic.Item.Weapon
{
    /// <summary>
    /// 射程看作持续时间的穿透子弹
    /// </summary>
    public class BeamProjecitle : Projectile
    {
        private float shootStartTime;

        public override void Initialize()
        {
            collider.enabled = false;
        }

        private void Update()
        {
            if(Time.time - shootStartTime > range)
            {
                OnDead();
            }
        }

        public override void Launch(Vector2 position, Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.zero, direction);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            collider.enabled = true;
            shootStartTime = Time.time;
        }

        protected override void OnHit(GameObject hitObject, Vector3 hitPos, Vector3 hitDirection)
        {
            HitBehavior hit = hitObject.GetComponent<HitBehavior>();
            Stats stat = hitObject.GetComponent<Stats>();
            if (hit != null && stat != null)
            {
                damage.DealDamage(hit, stat, hitDirection.normalized);
            }
        }
    }
}