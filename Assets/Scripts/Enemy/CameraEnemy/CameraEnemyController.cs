﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityBehavior;

/// <summary>
/// 摄像机敌人控制、攻击
/// </summary>
public class CameraEnemyController : MonoBehaviour
{
    public CameraSearchTarget seaTarget;
    public float rayTime = 1f;
    public float attackTime = 1f;
    public float attackCooldownTime = 5f;

    private ShootingBehavior shootingBehavior;
    private bool hasStartShooting;
    private Vector2 shootingDirection;
    [SerializeField] private Transform shootingPoint;

    private void Awake()
    {
        shootingBehavior = GetComponent<ShootingBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasStartShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartShooting)
        {
            //Debug.Log("laser" + seaTarget.targetPosition);
            hasStartShooting = !shootingBehavior.Shoot(shootingDirection);
            seaTarget.canAttack = false;
            seaTarget.canChangePosition = true;
        }
        else if(seaTarget.canAttack)
        {
            //Debug.Log("FindEnemy");
            shootingDirection = seaTarget.targetPosition - shootingPoint.position;
            hasStartShooting = true;
        }
    }
}
