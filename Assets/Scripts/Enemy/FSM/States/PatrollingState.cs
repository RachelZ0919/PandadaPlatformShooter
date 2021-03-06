﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态
    /// </summary>
    public class PatrollingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.isPatrolComplete = false;
        }

        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);

            switch (fsm.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsm);
                    break;
                case PatrolMode.loop:
                    LoopPatrolling(fsm);
                    break;
                case PatrolMode.PingPong:
                    PingPongPatrolling(fsm);
                    break;
            }
        }

        private int index;
        private void LoopPatrolling(FSMBase fsm)
        {
            if (fsm.wayPoints[index].GetComponent<Collider2D>().OverlapPoint(fsm.transform.position))
            {
                index = (index + 1) % fsm.wayPoints.Length;
            }
            fsm.canMove = true;
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private void PingPongPatrolling(FSMBase fsm)
        {
            if (fsm.wayPoints[index].GetComponent<Collider2D>().OverlapPoint(fsm.transform.position))
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    Array.Reverse(fsm.wayPoints);
                }
                index = (index + 1) % fsm.wayPoints.Length;
            }
            fsm.canMove = true;
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

        private void OncePatrolling(FSMBase fsm)
        {
            if (fsm.wayPoints[index].GetComponent<Collider2D>().OverlapPoint(fsm.transform.position))
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    fsm.isPatrolComplete = true;
                    return;
                }
                index++;
            }
            fsm.canMove = true;
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }
    }
}