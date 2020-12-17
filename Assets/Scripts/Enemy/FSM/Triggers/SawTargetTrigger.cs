using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 发现目标
    /// </summary>
    public class SawTargetTrigger : FSMTrigger
    {
        private bool isPlayer = false;
        private Vector3 rayDirection;
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (!fsm.targetTF)
                return false;
            //Debug.Log("发现player");
            rayDirection = fsm.targetTF.position - fsm.transform.position;
            LayerMask mask = 1 << 9 | 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(fsm.transform.position, rayDirection, 115, mask);
            if (hit)
            {
                //Debug.Log("检测到物体" + hit.collider.name);
                isPlayer = hit.transform.CompareTag("Player");
            }
            return isPlayer;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.SawTarget;
        }
    }
}
