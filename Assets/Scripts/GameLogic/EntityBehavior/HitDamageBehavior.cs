using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityStats.Damages;

namespace GameLogic.EntityBehavior
{
    public class HitDamageBehavior : MonoBehaviour
    {
        //伤害类型和数值
        [SerializeField] private DamageType damageType;
        [SerializeField] private float damageAmount;
        [SerializeField] private float hitDuration = 1f;

        private Collider2D collider;
        private float lastHitTime;


        //伤害
        private Damage damage;
        private Damage pureKnockBack;

        private void Awake()
        {
            damage = Damage.GetDamage(damageType);
            damage.damage = damageAmount;
            damage.knockbackForce = 15;

            pureKnockBack = Damage.GetDamage(DamageType.NormalDamage);
            damage.damage = 0;
            damage.knockbackForce = 10;

            collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if(gameObject.layer == LayerMask.NameToLayer("TiredEnemy") && Time.time - lastHitTime > 0.7f)
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(Time.time - lastHitTime > hitDuration)
            {
                int layermaskToHit = 1 << 8 | 1 << 9;
                layermaskToHit &= ~(1 << gameObject.layer);
                if (((1 << collision.gameObject.layer) & layermaskToHit) > 0)
                {
                    HitBehavior hitBehavior = collision.gameObject.GetComponent<HitBehavior>();
                    Stats stats = collision.gameObject.GetComponent<Stats>();
                    if (stats != null && hitBehavior != null)
                    {
                        Vector2 direction = collision.contacts[0].normal;
                        if (Mathf.Abs(direction.x) < 0.3f)
                        {
                            MovingBehavior movingBehavior = collision.gameObject.GetComponent<MovingBehavior>();
                            if (movingBehavior != null)
                            {
                                Debug.Log($"lastnotzero:{movingBehavior.lastNotZeroDirection}");
                                direction.x = movingBehavior.lastNotZeroDirection;
                            }
                            else
                            {
                                direction.x = collision.rigidbody.velocity.x;
                                direction.x /= Mathf.Abs(direction.x);
                                if (direction.x < 0.2f)
                                {
                                    direction.x = 1;
                                }
                            }
                        }

                        if(Mathf.Abs(direction.y) <= 0.1f)
                        {
                            damage.knockbackForce = 10;
                        }
                        else
                        {
                            damage.knockbackForce = 15;
                        }

                        lastHitTime = Time.time;
                        damage.DealDamage(hitBehavior, stats, direction);

                        gameObject.layer = LayerMask.NameToLayer("TiredEnemy");
                    }
                }
            }
            
        }

    }
}