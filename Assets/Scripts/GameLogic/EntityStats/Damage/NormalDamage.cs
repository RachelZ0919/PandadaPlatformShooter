using UnityEngine;
using GameLogic.EntityBehavior;

namespace GameLogic.EntityStats.Damages
{
    public class NormalDamage : Damage
    {
        public override void DealDamage(HitBehavior hitBehavior, Stats stat, Vector2 direction, bool isTouchDamage)
        {
            hitBehavior.GetHit(damage, knockbackForce, direction, isTouchDamage);
        }
    }
}