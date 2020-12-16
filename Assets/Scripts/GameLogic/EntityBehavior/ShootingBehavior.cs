using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.Item.Weapon;
using GameLogic.EntityStats;
using GameLogic.Managers;
using CameraLogic;

namespace GameLogic.EntityBehavior
{
    /// <summary>
    /// 人物射击基础属性:攻击、射速、子弹速度
    /// </summary>
    public struct ShootingBaseStats
    {
        /// <summary>
        /// 人物攻击值
        /// </summary>
        public float baseAttack;
        /// <summary>
        /// 人物射速
        /// </summary>
        public float baseSpeed;
        /// <summary>
        /// 人物子弹速度
        /// </summary>
        public float baseProjectileSpeed;
        /// <summary>
        /// 人物射程
        /// </summary>
        public float baseRange;
    }

    public class ShootingBehavior : MonoBehaviour
    {
        /// <summary>
        /// 实体当前持有的枪
        /// </summary>
        [HideInInspector] public Weapon weapon;
        [HideInInspector] public AudioManager audio;
        public Transform holdingPoint;
        public bool enableScreenShake = false;
        public bool enableAudio = true;
        private ShootingBaseStats baseStats; //基础属性

        private void Awake()
        {
            GetComponent<Stats>().OnStatsChanged += FetchEntityStats;
            baseStats = new ShootingBaseStats();
        }

        /// <summary>
        /// 让枪朝指定方向射出子弹
        /// </summary>
        /// <param name="direction">射击方向</param>
        public void Shoot(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (Mathf.Abs(angle) > 90) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
                holdingPoint.rotation = Quaternion.Euler(0, 0, angle - 180);
                
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                holdingPoint.rotation = Quaternion.Euler(0, 0, angle);
            }

            bool hasShot = weapon.Shoot(direction, baseStats);
            if (hasShot)
            {
                //抖屏
                if (enableScreenShake) CameraShake.instance.ShakeScreen(0.1f, 0.05f);
                //音效
                if (enableAudio && audio != null) audio.PlayAudio(weapon.shootingAudio);
            }



        }

        private void FetchEntityStats(Stats stat) //更新射击要用的属性
        {
            baseStats.baseAttack = stat.attack;
            baseStats.baseProjectileSpeed = stat.projectileSpeed;
            baseStats.baseSpeed = stat.shootingSpeed;
            baseStats.baseSpeed = stat.range;
        }
    }
}

