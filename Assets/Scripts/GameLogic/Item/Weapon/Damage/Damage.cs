using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;

namespace GameLogic.Item.Weapon
{
    public enum DamageType
    {
        NormalDamage = 1
    }
    /// <summary>
    /// 伤害类型基类
    /// </summary>
    abstract public class Damage
    {
        /// <summary>
        /// 基础伤害
        /// </summary>
        public float damage { get; set; }
        /// <summary>
        /// 计算伤害
        /// </summary>
        /// <param name="stat">人物属性</param>
        abstract public void DealDamage(Stats stat);
    }
}