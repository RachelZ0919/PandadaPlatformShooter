using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 离开攻击范围
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        private bool isPlayer2 = false;
        private Vector3 rayDirection2;
        bool ischange2;
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (!fsm.targetTF)
                return false;

            rayDirection2 = fsm.targetTF.position - fsm.transform.position;
            LayerMask mask2 = 1 << 9 | 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(fsm.transform.position, rayDirection2, 150, mask2);
            if (hit)
            {
                isPlayer2 = hit.transform.CompareTag("Player");
            }
            ischange2 = !isPlayer2 || (Vector3.Distance(fsm.transform.position, fsm.targetTF.position) > fsm.attackDistance);
            return ischange2;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }
}
