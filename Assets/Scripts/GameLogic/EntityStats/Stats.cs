﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic.EntityStats
{
    public class Stats : MonoBehaviour
    {
        #region Stats
        /// <summary>
        /// HP
        /// </summary>
        public float health
        {
            get
            {
                if (stats.ContainsKey("health"))
                {
                    return stats["health"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 攻击力
        /// </summary>
        public float attack
        {
            get
            {
                if (stats.ContainsKey("attack"))
                {
                    return stats["attack"];
                }
                else
                {
                    return 0;
                }

            }
        }
        /// <summary>
        /// 射速加成
        /// </summary>
        public float shootingSpeed
        {
            get
            {
                if (stats.ContainsKey("shootingSpeed"))
                {
                    return stats["shootingSpeed"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 子弹速度加成
        /// </summary>
        public float projectileSpeed
        {
            get
            {
                if (stats.ContainsKey("projectileSpeed"))
                {
                    return stats["projectileSpeed"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed
        {
            get
            {
                if (stats.ContainsKey("speed"))
                {
                    return stats["speed"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 射程加成
        /// </summary>
        public float range
        {
            get
            {
                if (stats.ContainsKey("range"))
                {
                    return stats["range"];
                }
                else
                {
                    return 0;
                }
            }
        }

        Dictionary<string, float> stats;
        #endregion

        #region Observer interface
        public delegate void StatsChanged(Stats stat);
        public StatsChanged OnStatsChanged;
        #endregion

        private void Awake()
        {
            stats = new Dictionary<string, float>();
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="value"></param>
        public void SetValue(string name, float value)
        {
            if (stats.ContainsKey(name))
            {
                stats[name] = value;
            }
            OnStatsChanged(this);
        }

        public void AddProperty(string name,float value)
        {
            if (stats.ContainsKey(name))
            {
                stats[name] = value;
            }
            else
            {
                stats.Add(name, value);
            }
            OnStatsChanged(this);
        }

        /// <summary>
        /// 初始化所有属性
        /// </summary>
        public void InitializeStats(StatData statData)
        {
            AddProperty("health", statData.health);
            AddProperty("attack", statData.attack);
            AddProperty("shootingSpeed", statData.shootingSpeed);
            AddProperty("projectileSpeed", statData.projectileSpeed);
            AddProperty("speed", statData.speed);
            AddProperty("range", statData.range);
        }

    }
}
