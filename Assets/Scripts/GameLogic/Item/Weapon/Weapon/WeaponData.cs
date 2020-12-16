using UnityEngine;
using System.Collections;

namespace GameLogic.Item.Weapon
{
    /// <summary>
    /// 武器基本数据
    /// </summary>
    public class WeaponData : ScriptableObject
    {
        /// <summary>
        /// 武器名字
        /// </summary>
        public string weaponName;

        /// <summary>
        /// 弹夹子弹数量
        /// </summary>
        public int projectilesPerClip;

        /// <summary>
        /// 冷却时间
        /// </summary>
        public float cooldownTime;

        /// <summary>
        /// 武器攻击力
        /// </summary>
        public float attack;

        /// <summary>
        /// 子弹速度
        /// </summary>
        public float projectileSpeed;

        /// <summary>
        /// 射速 = 每秒射击的子弹数量
        /// </summary>
        public float shootingSpeed;

        /// <summary>
        /// 射击距离
        /// </summary>
        public float range;

        /// <summary>
        /// 后坐力大小
        /// </summary>
        public float recoilForce;

        /// <summary>
        /// 击退强度
        /// </summary>
        public float knockbackForce;
    }
}