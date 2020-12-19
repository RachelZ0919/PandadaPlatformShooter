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
        private ShootingBaseStats baseStats; //基础属性

        #region Visual 

        public Transform holdingPoint;
        [SerializeField] private Transform shootingVisualTransform;

        public bool enableScreenShake = false;
        public float screenShakeIntensity = 0.05f;
        public float screenShakeTime = 0.1f;

        private Vector3 originScale;
        private Vector3 reverseScale;

        #endregion

        #region Audio

        [HideInInspector] public AudioManager audio;
        public bool enableAudio = true;

        #endregion



        private void Awake()
        {
            //设置回调
            GetComponent<Stats>().OnStatsChanged += FetchEntityStats;

            //基础属性
            baseStats = new ShootingBaseStats();

            //视觉
            originScale = shootingVisualTransform.localScale;
            originScale.x = Mathf.Abs(originScale.x);
            reverseScale = new Vector3(-originScale.x, originScale.y, originScale.z);
        }

        private void OnDisable()
        {
            //重设武器
            weapon = null;
        }

        /// <summary>
        /// 让枪朝指定方向射出子弹
        /// </summary>
        /// <param name="direction">射击方向</param>
        /// <returns>是否发射成功</returns>
        public bool Shoot(Vector2 direction)
        {
            if(weapon == null)
            {
                return false;
            }

            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (Mathf.Abs(angle) > 90) 
            {
                shootingVisualTransform.localScale = reverseScale;
                holdingPoint.rotation = Quaternion.Euler(0, 0, angle - 180);
                
            }
            else
            {
                shootingVisualTransform.localScale = originScale;
                holdingPoint.rotation = Quaternion.Euler(0, 0, angle);
            }

            bool hasShot = weapon.Shoot(direction, baseStats);
            if (hasShot)
            {
                //抖屏
                if (enableScreenShake) CameraShake.instance.ShakeScreen(screenShakeTime, screenShakeIntensity);
                //音效
                if (enableAudio && audio != null) audio.PlayAudio(weapon.shootingAudio);
            }

            return hasShot;
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

