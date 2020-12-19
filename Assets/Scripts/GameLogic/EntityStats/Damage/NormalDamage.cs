using UnityEngine;
using GameLogic.EntityBehavior;

namespace GameLogic.EntityStats.Damages
{
    public class NormalDamage : Damage
    {
        public override void DealDamage(HitBehavior hitBehavior, Stats stat, Vector2 direction)
        {
            hitBehavior.GetHit(damage, knockbackForce, direction);
        }
    }
}