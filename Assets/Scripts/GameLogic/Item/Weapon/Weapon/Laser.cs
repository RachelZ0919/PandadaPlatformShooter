using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;

namespace GameLogic.Item.Weapon
{
    public class Laser : Weapon
    {
        public override bool Shoot(Vector2 direction, ShootingBaseStats baseStats)
        {
            if (!isReloading && Time.time - lastShootingTime > 1 / (weaponData.shootingSpeed + baseStats.baseSpeed))
            {
                Projectile projectile = ProjectilePool.instance.SpawnAProjectile(weaponData.weaponName);

                //射子弹
                projectile.layermaskToHit = 1 << 8 | 1 << 9 | 1 << 10;
                projectile.layermaskToHit &= ~(1 << transform.parent.gameObject.layer);
                projectile.damage.damage = weaponData.attack + baseStats.baseAttack;
                projectile.damage.knockbackForce = weaponData.knockbackForce;
                projectile.speed = weaponData.projectileSpeed + baseStats.baseProjectileSpeed;
                projectile.range = weaponData.range + baseStats.baseRange;
                projectile.Launch(shootingPoint.position, direction);

                //后坐力
                ApplyRecoilForce();

                lastShootingTime = Time.time;
                projectileLeft--;
                if (projectileLeft <= 0)
                {
                    Reload();
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}