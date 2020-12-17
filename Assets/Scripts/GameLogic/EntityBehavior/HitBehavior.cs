﻿using UnityEngine;
using GameLogic.EntityStats;
using GameLogic.Managers;

namespace GameLogic.EntityBehavior
{
    public class HitBehavior : MonoBehaviour
    {
        /// <summary>
        /// 击中后无敌状态时间
        /// </summary>
        public float indivisibleDuration = 0.5f;
        /// <summary>
        /// 是否受击退效果
        /// </summary>
        public bool canGetKnockbacked = false;

        private Stats stat;
        private float hitStartTime;
        private Rigidbody2D rigidbody;
        private Animator animator;

        private Vector3 recoverOffset;
        private Vector3 currentOffset;
        private Vector3 lastOffset;
        private Vector3 recoverVel;

        [HideInInspector]public AudioManager audio;
        

        private void Awake()
        {
            stat = GetComponent<Stats>();
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (Vector3.Distance(recoverOffset, currentOffset) > 0.01f) 
            {
                currentOffset = Vector3.SmoothDamp(currentOffset, recoverOffset, ref recoverVel, 0.1f);
                transform.position += currentOffset - lastOffset;
                lastOffset = currentOffset;
            }
        }

        //todo:异常状态
        public void GetHit(float damage, float knockbackForce, Vector2 direction) {

            if (Time.time - hitStartTime >= indivisibleDuration)
            {
                Debug.Log(name + "isHit");
                stat.SetValue("health", stat.health - damage);

                //击退
                if (canGetKnockbacked)
                {
                    Debug.Log("getKnockbacked");
                    float knockBack = (Mathf.Max(0, knockbackForce - stat.knockBackResist) + 0.5f) * 0.5f;
                    recoverOffset = direction.normalized * knockBack;
                    transform.position -= recoverOffset * 1.5f;
                    lastOffset = currentOffset = Vector3.zero;
                }

                //视觉
                animator.SetBool("isHit", true);

                //无敌状态
                hitStartTime = Time.time;

                //音效
                audio.PlayAudio("hitAudio");
            }
        }

        private void LateUpdate()
        {
            animator.SetBool("isHit", false);
        }

    }
}