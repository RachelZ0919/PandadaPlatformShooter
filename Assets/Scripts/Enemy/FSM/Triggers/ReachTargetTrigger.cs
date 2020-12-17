using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标进入攻击范围
    /// </summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        private bool isPlayer3 = false;
        private Vector3 rayDirection3;
        bool ischange3;
        public override bool HandleTrigger(FSMBase fsm)
        {   //
            if (fsm.targetTF == null)
                return false;
            rayDirection3 = fsm.targetTF.position - fsm.transform.position;
            LayerMask mask = 1 << 9 | 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(fsm.transform.position, rayDirection3, 115, mask);
            if (hit)
            {
                //Debug.Log("检测到物体" + hit.collider.name);
                isPlayer3 = hit.transform.CompareTag("Player");
            }
            return isPlayer3 && Vector3.Distance(fsm.transform.position, fsm.targetTF.position) <= fsm.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }
}
