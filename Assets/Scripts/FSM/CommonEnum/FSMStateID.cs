using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态编号
    /// </summary>
    public enum FSMStateID
    {
        None,        //不存在此状态
        Default,     //默认
        Dead,        
        Idle,        
        Pursuit,     //追逐
        Attacking,   
        Patrolling   //巡逻
    }
}