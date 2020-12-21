using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityBehavior;

namespace AI.FSM
{
    /// <summary>
    /// 状态
    /// </summary>
    public class AttackingState : FSMState
    {
        
        public override void Init()
        {
            StateID = FSMStateID.Attacking;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            //fsm.timer = 0;
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            //fsm.timer += Time.deltaTime;
            fsm.anim.SetBool("isAttack", true);

            //if (fsm.timer > fsm.holdingTime && !fsm.isAttacking)
            //{
            //    fsm.isAttacking = true;
            //    //Vector3 direction = fsm.targetTF.position - fsm.transform.position;
            //    //fsm.shootingBehavior.Shoot(direction);
            //}
            //else if (fsm.timer > fsm.attackTime && fsm.isAttacking)
            //{
            //    fsm.timer = 0;
            //    fsm.isAttacking = false;
            //    fsm.anim.SetBool("isAttack", false);
            //}
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool("isAttack", false);
        }

        
    }
}