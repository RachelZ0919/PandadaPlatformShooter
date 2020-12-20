using System.Collections;
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
    public float appearTime;
    public float disappearTime;
    private bool isReadyAttack = false;
    private bool isDisappear = true;
    private bool isAttacking = false;
    private float timer = 0;
    public float moveSpeed = 4f;
    private int randomPoint = -1;
    private int length;
    private Vector3 lookPosition;

    public Transform[] movePoints;
    private Vector3 moveDirection;
    private Vector3 lookDirection;
    private Vector3 targetPosition;

    private float targetAngle;
    private float currentAngle;
    private float dampVelocity;

    [SerializeField] private Transform spriteTransform;

    private void Awake()
    {
        shootingBehavior = GetComponent<ShootingBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasStartShooting = false;
        length = movePoints.Length / 2;
        randomPoint = Random.Range(0, length);
        transform.position = movePoints[randomPoint * 2].position;
        currentAngle = targetAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //消失阶段
        if (timer < disappearTime && isDisappear)
        {
            timer += Time.deltaTime;
        }
        else if(timer > disappearTime && isDisappear && !isReadyAttack)
        {
            isDisappear = false;
            isReadyAttack = false;
            timer = 0;
        }

        //移动阶段
        if (!isDisappear && !isReadyAttack)
        {
            //移动到目标位置
            if (Vector3.Distance(transform.position, movePoints[randomPoint * 2 + 1].position) > 0.1f)
            {
                MoveToTarget(movePoints[randomPoint * 2 + 1].position, moveSpeed);
                lookPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

                //更改摄像机朝向
                lookDirection = (transform.position - lookPosition);
                lookDirection.z = 0;
                //Debug.Log(lookDirection);
                if (lookDirection.x > 0)
                {
                    spriteTransform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                else if(lookDirection.x < 0)
                {
                    spriteTransform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

                }

                //更改图片方向
                float angle = Vector2.SignedAngle(Vector2.right, lookDirection);
                if (Mathf.Abs(angle) > 90)
                {
                    transform.rotation = Quaternion.Euler(0, 0, angle - 180);

                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
            else
            {
                isReadyAttack = true;
                isDisappear = false;
                timer = 0;
            }
        }

        //攻击阶段
        if (!isDisappear && isReadyAttack && timer < appearTime)
        {            
            timer += Time.deltaTime;
            Attack();
        }
        else if(!isDisappear && isReadyAttack && timer > appearTime)
        {
            isAttacking = false;
            isReadyAttack = false;
            isDisappear = true;
            timer = 0;
            randomPoint = Random.Range(0, length);
            transform.position = movePoints[randomPoint * 2].position;
        }
    }

    public void MoveToTarget(Vector3 position, float speed)
    {
        moveDirection = (position - transform.position).normalized;
        this.transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            isAttacking = true;
            hasStartShooting = true;
        }
        else if (hasStartShooting && isAttacking)
        {
            shootingDirection = targetPosition - shootingPoint.position;
            hasStartShooting = !shootingBehavior.Shoot(shootingDirection);
        }
    }
}
