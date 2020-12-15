using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;

namespace GameLogic.Item.Weapon
{
    abstract public class ProjectileSpawner
    {
        public string projecilePool;

        abstract public void SpawnProjectile(Vector2 direction, ShootingBaseStats baseStats);
    }
}