using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SquarePatrolAction : Action
{
    public SharedFloat moveSpeed;
    public SharedTransform[] wayPoints;
    private int index;

    public override TaskStatus OnUpdate()
    {

        if (wayPoints[index].Value.GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            index = (index + 1) % wayPoints.Length;
        }
        if (wayPoints[index] == null || wayPoints[index].Value == null)
        {
            return TaskStatus.Failure;
        }

        transform.position = Vector3.MoveTowards(transform.position,
            wayPoints[index].Value.position, moveSpeed.Value * Time.deltaTime);
        return TaskStatus.Success;
    }
}
