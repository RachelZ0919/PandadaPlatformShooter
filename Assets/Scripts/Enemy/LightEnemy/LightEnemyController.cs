using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyController : MonoBehaviour
{
    private int index;
    [Tooltip("巡逻点")]
    public Transform[] wayPoints;
    public FanSearchtarget fanTarget;
    public float moveSpeed = 10f;
    public float minAttackColldownTime = 2f;
    public float maxAttackColldownTime = 5f;
    private float randTimer = 3f;
    public float standTime;
    public float BoombTime;
    public float stopDistance;

    private Transform target;
    private bool canAttack;
    private Vector3 targetPosition;
    private bool isRush;
    private bool isBoomb;

    void Update()
    {      
        if (isBoomb && !isRush)
        {

        }
        else if (isRush && !isBoomb)
        {

        }
        else if (canAttack && !isRush && isBoomb)
        {
            SearchTarget();
        }
        else if (randTimer < 0)
        {
            randTimer = Random.Range(minAttackColldownTime, maxAttackColldownTime);
            canAttack = true;
        }
        else
        {
            randTimer -= Time.deltaTime;
            isRush = false;
            isBoomb = false;
        }        

        if (isRush)
        {
            //攻击
            Attack();
            Debug.Log("Attack");
        }
        else
        {
            LoopPatrolling();
        }
    }

    private void SearchTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(target.name);
        if (target)
        {
            targetPosition = target.transform.position;
            canAttack = true;
            isRush = true;
        }
    }

    private void LoopPatrolling()
    {
        if (wayPoints[index].GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            index = (index + 1) % wayPoints.Length;
        }
        //Debug.Log("move to " + index);
        MoveToTarget(wayPoints[index].position, moveSpeed);
    }

    public void MoveToTarget(Vector3 position, float speed)
    {
        Vector3 moveDirection = (position - transform.position).normalized;
        this.transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void Attack()
    {

    }

    private void Rush()
    {

    }
}
