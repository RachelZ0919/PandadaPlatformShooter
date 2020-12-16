using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic.EntityStats
{
    /// <summary>
    /// 人物属性类
    /// </summary>
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
        /// <summary>
        /// 最大生命值
        /// </summary>
        public float maxHealth
        {
            get
            {
                if (stats.ContainsKey("maxHealth"))
                {
                    return stats["maxHealth"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 击退抗性
        /// </summary>
        public float knockBackResist
        {
            get
            {
                if (stats.ContainsKey("knockbackResist"))
                {
                    return stats["knockbackResist"];
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 属性字典
        /// </summary>
        private Dictionary<string, float> stats;
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

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">值</param>
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
        /// 获取属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public float GetProperty(string name)
        {
            if (stats.ContainsKey(name))
            {
                return stats[name];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 初始化所有属性
        /// </summary>
        public void InitializeStats(StatData statData)
        {
            AddProperty("health", statData.health);
            AddProperty("maxHealth", statData.health);
            AddProperty("attack", statData.attack);
            AddProperty("shootingSpeed", statData.shootingSpeed);
            AddProperty("projectileSpeed", statData.projectileSpeed);
            AddProperty("speed", statData.speed);
            AddProperty("range", statData.range);
        }

    }
}
