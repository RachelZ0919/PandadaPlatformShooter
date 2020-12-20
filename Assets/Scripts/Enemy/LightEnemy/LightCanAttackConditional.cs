using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCanAttackConditional : Conditional
{
    public float fildOfViewAngle = 90;

    public SharedVector3 attackPosition;
    public SharedFloat attackCoolDownTime = 4f;
    public SharedBool isAttacking = false;
    public PatrolAction patAc;

    //行为树局部Transform变量
    public Transform target;

    public override TaskStatus OnUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null && attackCoolDownTime.Value < patAc.timer.Value)
        {
            target = null;
            patAc.timer.Value = 0;
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }

}
