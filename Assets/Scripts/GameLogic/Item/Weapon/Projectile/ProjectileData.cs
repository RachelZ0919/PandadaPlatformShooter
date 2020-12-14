using UnityEngine;
using System.Collections;

namespace GameLogic.Item.Weapon
{
    public enum ProjectileType
    {
        Straight = 1
    }

    /// <summary>
    /// 用来生成子弹的数据，在WeaponData内，但projectile本身不用这些数据。
    /// </summary>
    public class ProjectileData : ScriptableObject
    {
        /// <summary>
        /// 子弹样式
        /// </summary>
        public Sprite projectileImage; //todo:支持子弹动画
        /// <summary>
        /// 子弹伤害类型
        /// </summary>
        public DamageType damageType;
        /// <summary>
        /// 子弹类型
        /// </summary>
        public ProjectileType type;

        public Projectile GenerateProjectile()
        {
            GameObject projectile = new GameObject();
            projectile.AddComponent<Rigidbody2D>();
            SpriteRenderer spriteRenderer = projectile.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = projectileImage;
            switch (type)
            {
                case ProjectileType.Straight:
                    {
                        Projectile proj = projectile.AddComponent<StraightProjectile>();
                        proj.damage = GetDamage();
                        return proj;
                    }
            }
            return null;
        }

        private Damage GetDamage()
        {
            switch (damageType)
            {
                case DamageType.NormalDamage:
                    {
                        return new NormalDamage();
                    }
            }
            return null;
        }
    }
}