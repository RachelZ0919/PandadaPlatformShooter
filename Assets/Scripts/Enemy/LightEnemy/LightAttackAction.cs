using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightAttackAction : Action
{
    /*
    public SharedFloat attackTime;
    private float timer = 0;
    public LightCanAttackConditional lightCanAtt;

    public override void OnStart()
    {
        base.OnStart();
        timer = 0;
    }

    public override TaskStatus OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > attackTime.Value)
        {
            timer = 0;
            lightCanAtt.isAttacking.Value = false;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    */

    public bool isBoomb;
    public LightBeforeAttackAction lightBefor;
    public LightRushAction lightRush;
    public SharedGameObject lightOut;
    public Animator anim;
    public float animTime;
    private float animTimer = 0;
    private CircleCollider2D CirColl;
    private float oriRadius;

    public override void OnStart()
    {
        CirColl = GetComponent<CircleCollider2D>();
        oriRadius = CirColl.radius;
        CirColl.radius = 2.7f;
        base.OnStart();
        isBoomb = true;
        anim = lightOut.Value.GetComponent<Animator>();
        anim.SetBool("isBoomb", isBoomb);
    }

    public override TaskStatus OnUpdate()
    {
        if (animTimer < animTime)
        {
            animTimer += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {
            lightRush.isRush = false;
            lightRush.isFirst = true;
            lightBefor.isHolding = false;
            isBoomb = false;
            animTimer = 0;
            CirColl.radius = oriRadius;
            return TaskStatus.Success;
        }
    }
}
