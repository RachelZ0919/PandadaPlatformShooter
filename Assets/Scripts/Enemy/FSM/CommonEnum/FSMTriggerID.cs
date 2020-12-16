using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态编号
    /// </summary>
    public enum FSMTriggerID
    {
        NoHealth,        
        SawTarget,     //发现目标
        ReachTarget,        //到达目标
        KilledTarget,        
        WithoutAttackRange,     //超出攻击范围
        LoseTarget,   //丢失目标
        CompletePatrol   //完成巡逻
    }
}