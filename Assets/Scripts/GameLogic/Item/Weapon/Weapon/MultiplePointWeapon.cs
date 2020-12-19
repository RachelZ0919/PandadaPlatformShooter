﻿using UnityEngine;
using System.Collections.Generic;
using GameLogic.EntityBehavior;
using VisualEffect;

namespace GameLogic.Item.Weapon
{
    public class MultiplePointWeapon : Weapon
    {
        /// <summary>
        /// 发射点,旋转(0,0,0)的时候默认向右
        /// </summary>
        [SerializeField] private List<Transform> shootingPoints;

        public override bool Shoot(Vector2 direction, ShootingBaseStats baseStats)
        {
            if (!isReloading && Time.time - lastShootingTime > 1 / (weaponData.shootingSpeed + baseStats.baseSpeed))
            {
                for(int i = 0; i < shootingPoints.Count; i++)
                {
                    Projectile projectile = GetAProjectile(baseStats);
                    //float angle = shootingPoints[i].eulerAngles.z * Mathf.Deg2Rad;
                    //Vector2 directionVec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    Vector2 shootdir = shootingPoints[i].localRotation * direction;

                    projectile.Launch(shootingPoints[i].position, shootdir);

                    if (effectName != null) EffectPool.instance.PlayEffect(effectName, shootingPoints[i].position, shootdir);
                }

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