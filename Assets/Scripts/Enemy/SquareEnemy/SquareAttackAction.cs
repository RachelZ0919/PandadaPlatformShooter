using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameLogic.EntityBehavior;
using UnityEngine;

public class SquareAttackAction : Action
{
    public SquareCanAttackConditional canAttCon;
    public float attackReadyTime;
    private float timer = 0;

    public override TaskStatus OnUpdate()
    {
        if (timer < attackReadyTime)
        {
            timer += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {
            if(canAttCon.targetPosition == null)
            {
                return TaskStatus.Failure;
            }
            GetComponent<ShootingBehavior>().Shoot(canAttCon.targetPosition - transform.position);
            return TaskStatus.Success;
        }
    }
}
