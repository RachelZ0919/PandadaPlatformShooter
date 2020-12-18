using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 追逐状态
    /// </summary>
    public class PursuitState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            fsm.canMove = true;
            fsm.MoveToTarget(fsm.targetTF.position, fsm.attackDistance, fsm.runSpeed);
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            //fsm.StopMove();
        }


    }
}