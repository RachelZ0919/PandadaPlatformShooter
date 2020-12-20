using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LightBeforeAttackAction : Action
{
    public SharedFloat holdingTime;
    private float timer = 0;
    public bool isHolding = false;
    public SharedGameObject lightOut;
    public Animator anim;

    public override void OnStart()
    {
        //base.OnStart();
        isHolding = true;
        anim = lightOut.Value.GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        anim.SetBool("isHolding", isHolding);
        timer += Time.deltaTime;
        if (timer < holdingTime.Value)
        {
            return TaskStatus.Running;
        }        
        else
        {
            timer = 0;
            return TaskStatus.Success;
        }
    }
}
