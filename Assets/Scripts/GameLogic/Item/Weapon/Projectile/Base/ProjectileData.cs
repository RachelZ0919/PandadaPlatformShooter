﻿using UnityEngine;
using System.Collections;

namespace GameLogic.Item.Weapon
{
    public enum ProjectileType
    {
        Straight = 1,
        Beam = 2
    }

    public enum ColliderType
    {
        Circle = 1,
        Box = 2
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
        /// 子弹碰撞体类型
        /// </summary>
        public ColliderType colliderType;
        /// <summary>
        /// 子弹大小
        /// </summary>
        public float size;

        public Projectile GenerateProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab);

            //设置图片
            Transform spriteTransform = projectile.transform.Find("sprite");
            SpriteRenderer projectileSprite = spriteTransform.GetComponent<SpriteRenderer>();
            projectileSprite.sprite = projectileImage;

            //设置子弹大小
            spriteTransform.localScale = new Vector3(size, size, 1);

            //设置碰撞体
            Collider2D collider = null;
            switch (colliderType)
            {
                case ColliderType.Circle:
                    {
                        CircleCollider2D circleCollider = spriteTransform.gameObject.AddComponent<CircleCollider2D>();
                        collider = circleCollider;
                        break;
                    }
                case ColliderType.Box:
                    {
                        BoxCollider2D boxCollider = spriteTransform.gameObject.AddComponent<BoxCollider2D>();
                        collider = boxCollider;
                        break;
                    }
            }

            collider.isTrigger = true;

            switch (type)
            {
                case ProjectileType.Straight:
                    {
                        Projectile proj = projectile.AddComponent<StraightProjectile>();
                        proj.damage = GetDamage();
                        proj.collider = collider;
                        return proj;
                    }
                case ProjectileType.Beam:
                    {
                        Projectile proj = projectile.AddComponent<BeamProjecitle>();
                        proj.damage = GetDamage();
                        proj.collider = collider;
                        projectileSprite.drawMode = SpriteDrawMode.Sliced;
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