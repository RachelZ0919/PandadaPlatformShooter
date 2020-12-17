using UnityEngine;
using System.Collections;

namespace GameLogic.Item.Weapon
{
    public enum ProjectileType
    {
        Straight = 1,
        Beam = 2
    }

    /// <summary>
    /// 用来生成子弹的数据，在WeaponData内，但projectile本身不用这些数据。
    /// </summary>
    public class ProjectileData : ScriptableObject
    {
        [HideInInspector] public GameObject projectilePrefab;
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
        /// <summary>
        /// 子弹碰撞体（圆形）半径
        /// </summary>
        public float size;

        public Projectile GenerateProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab);

            //设置图片
            SpriteRenderer projectileSprite = projectile.transform.Find("sprite").GetComponent<SpriteRenderer>();
            projectileSprite.sprite = projectileImage;

            //设置碰撞体大小
            CircleCollider2D collider = projectile.GetComponent<CircleCollider2D>();
            collider.radius = size;

            switch (type)
            {
                case ProjectileType.Straight:
                    {
                        Projectile proj = projectile.AddComponent<StraightProjectile>();
                        proj.damage = GetDamage();
                        return proj;
                    }
                case ProjectileType.Beam:
                    {
                        Projectile proj = projectile.AddComponent<BeamProjecitle>();
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