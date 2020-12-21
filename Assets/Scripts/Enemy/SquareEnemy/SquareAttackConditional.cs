using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCanAttackConditional : Conditional
{

    //行为树局部Transform变量
    private GameObject target;
    public float searchDistance;
    public float attackCoolDownTime = 4f;
    private float CoolTimer = 0;
    public Vector3 targetPosition;

    public override TaskStatus OnUpdate()
    {

        //player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
        //{
        //    target = player.Value.transform;
        //}
        target = GameObject.FindGameObjectWithTag("Player");
        
        if (target != null && attackCoolDownTime < CoolTimer
            && Vector3.Distance(transform.position, target.transform.position) < searchDistance)
        {
            targetPosition = target.transform.position;
            CoolTimer = 0;
            return TaskStatus.Success;
        }
        CoolTimer += Time.deltaTime;
        return TaskStatus.Failure;

        //SearchTarget();
        //if (target != null && attackCoolDownTime < CoolTimer)
        //{
        //    CoolTimer = 0;
        //    return TaskStatus.Success;
        //}
        //CoolTimer += Time.deltaTime;
        //return TaskStatus.Failure;

    }

    //private void SearchTarget()
    //{
    //    List<Transform> targetArr = new List<Transform>();
    //    //添加所有Player进链表
    //    GameObject[] tempGoArrat = GameObject.FindGameObjectsWithTag("Player");
    //
    //    //找到最近的player。
    //    targetArr = targetArr.FindAll(t => Vector3.Distance(t.position, this.transform.position) <= this.searchDistance);
    //    Transform[] result = targetArr.ToArray();
    //    if (result.Length == 0)
    //    {
    //        target = null;
    //    }
    //    else
    //    {
    //        target = result[0];
    //    }
    //}
}

