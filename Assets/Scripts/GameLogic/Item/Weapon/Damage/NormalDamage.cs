using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;
using GameLogic.EntityStats;

namespace GameLogic.Item.Weapon
{
    public class NormalDamage : Damage
    {
        public override void DealDamage(HitBehavior hitBehavior, Stats stat, Vector2 direction)
        {
            hitBehavior.GetHit(damage, knockbackForce, direction);
        }
    }
}