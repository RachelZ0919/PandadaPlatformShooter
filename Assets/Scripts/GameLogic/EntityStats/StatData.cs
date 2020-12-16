using UnityEngine;
using System.Collections;

namespace GameLogic.EntityStats
{
    public class StatData : ScriptableObject
    {
        /// <summary>
        /// HP
        /// </summary>
        public float health;
        /// <summary>
        /// 攻击力
        /// </summary>
        public float attack;
        /// <summary>
        /// 基础射速
        /// </summary>
        public float shootingSpeed;
        /// <summary>
        /// 基础弹速
        /// </summary>
        public float projectileSpeed;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed;
        /// <summary>
        /// 射程
        /// </summary>
        public float range;
        /// <summary>
        /// 击退抗性
        /// </summary>
        public float knockbackResist;
        /// <summary>
        /// 初始武器
        /// </summary>
        public GameObject defaultWeapon;
    }
}