using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.Item.Weapon;
using GameLogic.EntityStats;

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
        public Weapon weapon;
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
            weapon.Shoot(direction, baseStats);
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

