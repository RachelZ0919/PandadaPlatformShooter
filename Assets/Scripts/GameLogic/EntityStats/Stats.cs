using UnityEngine;
using System.Collections;

namespace GameLogic.EntityStats
{
    abstract public class Stats : MonoBehaviour
    {
        #region Stats
        /// <summary>
        /// HP
        /// </summary>
        public float health
        {
            get
            {
                return health;
            }
            set
            {
                health = Mathf.Max(0, value);
                OnStatsChanged(this);
            }
        }
        /// <summary>
        /// 攻击力
        /// </summary>
        public float attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
                OnStatsChanged(this);
            }
        }
        /// <summary>
        /// 射速加成
        /// </summary>
        public float shootingSpeed
        {
            get
            {
                return shootingSpeed;
            }
            set
            {
                shootingSpeed = value;
                OnStatsChanged(this);
            }
        }
        /// <summary>
        /// 子弹速度加成
        /// </summary>
        public float projectileSpeed
        {
            get
            {
                return projectileSpeed;
            }
            set
            {
                projectileSpeed = value;
                OnStatsChanged(this);
            }
        }
        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                OnStatsChanged(this);
            }
        }
        /// <summary>
        /// 射程加成
        /// </summary>
        public float range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
                OnStatsChanged(this);
            }
        }
        #endregion

        #region Observer interface
        public delegate void StatsChanged(Stats stat);
        public StatsChanged OnStatsChanged;
        #endregion

        /// <summary>
        /// 初始化所有属性
        /// </summary>
        abstract protected void InitializeStats();

        private void Start()
        {
            InitializeStats();
        }
    }
}