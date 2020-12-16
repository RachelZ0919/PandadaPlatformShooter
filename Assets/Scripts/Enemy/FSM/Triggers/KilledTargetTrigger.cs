using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityStats;

namespace AI.FSM
{
    /// <summary>
    /// 杀死目标
    /// </summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.targetTF == null)
                return false;
            return fsm.targetTF.GetComponent<Stats>().health <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.KilledTarget;
        }
    }
}
