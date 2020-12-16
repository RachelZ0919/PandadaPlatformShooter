using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 生命值为0
    /// </summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.chStatus.health <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }

    }
}
