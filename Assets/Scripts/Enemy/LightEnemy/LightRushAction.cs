using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightRushAction : Action
{
    public SharedFloat rushSpeed;
    //public LightCanAttackConditional canAttCon;
    public float distance;
    public bool isRush;
    public bool isFirst = true;
    public Vector3 attackPosition;
    public SharedGameObject lightOut;
    public LightBeforeAttackAction lBeforAtt;
    public Transform target;
    public Animator anim;

    public override void OnStart()
    {
        //base.OnStart();
        lBeforAtt.isHolding = false;
        anim = lightOut.Value.GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        anim.SetBool("isHolding", lBeforAtt.isHolding);
        //Debug.Log(canAttCon.attackPosition.Value);
        if (isFirst)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            attackPosition = target.position;
            isFirst = false;
            //canAttCon.target = null;
        }


        transform.position = Vector3.MoveTowards(transform.position,
            attackPosition, rushSpeed.Value * Time.deltaTime);

        //当前距离
        distance = (transform.position - attackPosition).sqrMagnitude;
        //Debug.Log(distance);
        if (distance <= 0.1f)
        {
            //到达目标点
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
