﻿using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;

namespace GameLogic.Item.Weapon
{
    public class NormalDamage : Damage
    {
        public override void DealDamage(Stats stat)
        {
            stat.health = stat.health - damage;
        }
    }
}