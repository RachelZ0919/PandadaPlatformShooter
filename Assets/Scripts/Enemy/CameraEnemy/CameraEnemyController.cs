using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机敌人控制、攻击
/// </summary>
public class CameraEnemyController : MonoBehaviour
{
    public SearchTarget seaTarget;
    public float rayTime = 1f;
    public float attackTime = 1f;
    public float attackCooldownTime = 5f;
    private bool isRaying = false;
    private bool isAttacking = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (seaTarget.canAttack)
        {
            if (timer > attackCooldownTime)
            {
                timer = 0;
                //jiguang
                Debug.Log("Ray");
                isRaying = true;
            }
            else if(timer >= 1f && isRaying)
            {
                isRaying = false;
                //gongji
                Debug.Log("attack");
                isAttacking = true;
            }
            else if (timer > 2f && isAttacking)
            {
                isAttacking = false;
                //stop
                Debug.Log("stop");
            }
            timer += Time.deltaTime;
        }
    }
}
