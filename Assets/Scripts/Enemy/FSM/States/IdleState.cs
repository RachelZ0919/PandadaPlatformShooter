using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态
    /// </summary>
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            Debug.Log("闲置");
            //播放动画
            //fsm.anim.SetBool(fsm.chStatus.chparams.idle, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            //播放动画
            //fsm.anim.SetBool(fsm.chStatus.chparams.idle, false);
        }


    }
}