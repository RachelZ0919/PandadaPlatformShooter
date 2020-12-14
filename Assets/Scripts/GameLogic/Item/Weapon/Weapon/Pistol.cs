﻿using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;

namespace GameLogic.Item.Weapon
{
    public class Pistol : Weapon
    {
        public override void Shoot(Vector2 direction, ShootingBaseStats baseStats)
        {
            if (!isReloading && Time.time - lastShootingTime > 1 / (weaponData.shootingSpeed + baseStats.baseSpeed)) 
            {
                Projectile projectile = ProjectilePool.instance.SpawnAProjectile(weaponData.weaponName);

                //todo：射子弹
                projectile.damage.damage = weaponData.attack + baseStats.baseAttack;
                projectile.speed = weaponData.projectileSpeed + baseStats.baseProjectileSpeed;
                projectile.range = weaponData.range + baseStats.baseRange;
                projectile.Launch(transform.position, direction);

                //todo: 后坐力

                lastShootingTime = Time.time;
                projectileLeft--;
                if(projectileLeft <= 0)
                {
                    Reload();
                }
            }
        }
    }
}