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
        private float attackTime;

        public override void Init()
        {
            StateID = FSMStateID.Attacking;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            Vector3 direction = fsm.targetTF.position - fsm.transform.position;
            fsm.shootingBehavior.Shoot(direction);

            //if (attackTime <= Time.time)
            //{
            //    //攻击
            //    Debug.Log("开始攻击");

            //    //fsm.chStatus.Attack(fsm.targetTF.position);
            //    //attackTime = Time.time + fsm.chStatus.attackCooldown;
            //}
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
        }

        
    }
}