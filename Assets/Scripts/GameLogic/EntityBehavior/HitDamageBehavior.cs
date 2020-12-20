using UnityEngine;
using System.Collections.Generic;
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
        [SerializeField] private bool hasKnockBack = true;
        [SerializeField] private float noColliderDuration = 0.7f;
        [SerializeField] private float knockBackSmall = 6;
        [SerializeField] private float knockBackBig = 8;

        private float lastHitTime;
        private int originlayer;

        List<HitBehavior> objectToHit = new List<HitBehavior>();


        //伤害
        private Damage damage;

        private void Awake()
        {
            damage = Damage.GetDamage(damageType);
            damage.damage = damageAmount;
            originlayer = gameObject.layer;
        }

        private void Start()
        {
            lastHitTime = Time.time - hitDuration;
        }

        private void Update()
        {
            if (gameObject.layer == LayerMask.NameToLayer("TiredEnemy") && Time.time - lastHitTime > noColliderDuration) 
            {
                gameObject.layer = originlayer;
            }


        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            int layermaskToHit = 1 << 8 | 1 << 9;
            layermaskToHit &= ~(1 << gameObject.layer);
            if (((1 << collision.gameObject.layer) & layermaskToHit) > 0)
            {
                HitBehavior hitBehavior = collision.gameObject.GetComponent<HitBehavior>();
                if(hitBehavior != null)
                {
                    if (Time.time - lastHitTime > hitDuration && !hitBehavior.isIndivisible)
                    {
                        ExecuteHit(collision, hitBehavior);
                    }
                    else
                    {
                        objectToHit.Add(hitBehavior);
                    }
                }

            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Time.time - lastHitTime > hitDuration)
            {
                int layermaskToHit = 1 << 8 | 1 << 9;
                layermaskToHit &= ~(1 << gameObject.layer);
                if (((1 << collision.gameObject.layer) & layermaskToHit) > 0)
                {
                    HitBehavior hitBehavior = collision.gameObject.GetComponent<HitBehavior>();
                    if(hitBehavior != null)
                    {
                        for (int i = 0; i < objectToHit.Count; i++)
                        {
                            if (objectToHit[i] == hitBehavior)
                            {
                                if (!hitBehavior.isIndivisible)
                                {
                                    ExecuteHit(collision, hitBehavior);
                                    objectToHit.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }

                }
            }

        }

        private void ExecuteHit(Collision2D collision, HitBehavior hitBehavior)
        {
            Stats stats = collision.gameObject.GetComponent<Stats>();
            if (stats != null)
            {
                Vector2 direction = collision.contacts[0].normal;
                if (Mathf.Abs(direction.x) < 0.3f)
                {
                    MovingBehavior movingBehavior = collision.gameObject.GetComponent<MovingBehavior>();
                    if (movingBehavior != null)
                    {
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

                if (hasKnockBack)
                {
                    if (Mathf.Abs(direction.y) <= 0.1f)
                    {
                        damage.knockbackForce = knockBackSmall;
                    }
                    else
                    {
                        damage.knockbackForce = knockBackBig;
                        lastHitTime = Time.time;
                        gameObject.layer = LayerMask.NameToLayer("TiredEnemy");
                    }
                }
                else
                {
                    damage.knockbackForce = 0;
                }

                damage.DealDamage(hitBehavior, stats, direction, true);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            int layermaskToHit = 1 << 8 | 1 << 9;
            layermaskToHit &= ~(1 << gameObject.layer);
            if (((1 << collision.gameObject.layer) & layermaskToHit) > 0)
            {
                HitBehavior hitBehavior = collision.gameObject.GetComponent<HitBehavior>();
                if(hitBehavior != null)
                {
                    for (int i = 0; i < objectToHit.Count; i++)
                    {
                        if (objectToHit[i] == hitBehavior)
                        {
                            objectToHit.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        private void OnDisable()
        {
            objectToHit.Clear();
        }
    }
}