using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityBehavior;

public class FanEnemyController : MonoBehaviour
{
    [Tooltip("巡逻点")]
    public Transform[] wayPoints;
    [HideInInspector] public ShootingBehavior shootingBehavior;
    public FanSearchtarget fanTarget;
    public Transform fanSprite;//控制旋转
    public float moveSpeed = 10f;
    public float rotateSpeed = -4f;


    void Start()
    {
        shootingBehavior = GetComponent<ShootingBehavior>();
    }


    void Update()
    {
        if (fanTarget.canAttack)
        {
            //攻击
            shootingBehavior.Shoot(Vector2.down);
            fanSprite.transform.Rotate(0, 0, rotateSpeed * 4);
        }
        else 
        {
            LoopPatrolling();
        }
    }

    private int index;
    private void LoopPatrolling()
    {
        if (wayPoints[index].GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            index = (index + 1) % wayPoints.Length;
            rotateSpeed = -rotateSpeed;
        }
        Debug.Log("move to " + index);
        MoveToTarget(wayPoints[index].position);
        fanSprite.transform.Rotate(0, 0, rotateSpeed);
        //fanSprite.transform.Rotate(,);
    }

    public void MoveToTarget(Vector3 position)
    {
        Vector3 moveDirection = (position - transform.position).normalized;
        this.transform.position += moveDirection * moveSpeed * Time.deltaTime;      
    }
}
