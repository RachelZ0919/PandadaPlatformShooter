using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class PatrolAction : Action
{
    public SharedFloat moveSpeed;
    public SharedTransform[] wayPoints;
    public SharedFloat timer;
    public LightAttackAction lightAtt;
    public SharedGameObject lightOut;
    public Animator anim;
    private int index;

    public override void OnStart()
    {
        //base.OnStart();
        lightAtt.isBoomb = false;
        anim = lightOut.Value.GetComponent<Animator>();
        anim.SetBool("isBoomb", lightAtt.isBoomb);
    }
    public override TaskStatus OnUpdate()
    {
        
        if (wayPoints[index].Value.GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            index = (index + 1) % wayPoints.Length;
        }
        timer.Value += Time.deltaTime;
        if (wayPoints[index] == null || wayPoints[index].Value == null)
        {
            return TaskStatus.Failure;
        }

        transform.position = Vector3.MoveTowards(transform.position, 
            wayPoints[index].Value.position, moveSpeed.Value * Time.deltaTime);
        return TaskStatus.Success;
    }
}
