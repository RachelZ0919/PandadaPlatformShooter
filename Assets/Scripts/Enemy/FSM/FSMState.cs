using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.FSM
{
    /// <summary>
    /// 状态类
    /// </summary>
    public abstract class FSMState
    {
        /// <summary>
        /// 
        /// </summary>
        public FSMStateID StateID { get; set; }

        /// <summary>
        /// 触发器和状态
        /// </summary>
        private Dictionary<FSMTriggerID, FSMStateID> map;

        /// <summary>
        /// 触发器列表
        /// </summary>
        private List<FSMTrigger> Triggers;

        /// <summary>
        /// 构造FSMState
        /// </summary>
        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            Triggers = new List<FSMTrigger>();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 条件对象和状态机绑定
        /// </summary>
        /// <param name="triggerID">条件对象ID</param>
        /// <param name="stateID">状态机ID</param>
        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            map.Add(triggerID, stateID);
            CreateTrigger(triggerID);
        }

        /// <summary>
        /// 创建条件对象
        /// </summary>
        /// <param name="triggerID">条件对象ID</param>
        private void CreateTrigger(FSMTriggerID triggerID)
        {
            //命名规范：AI.FSM+条件枚举+Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            Triggers.Add(trigger);
        }

        /// <summary>
        /// 检测是否要切换状态
        /// </summary>
        /// <param name="fsm">使用的状态机</param>
        public void Reason(FSMBase fsm)
        {
            for (int i = 0; i < Triggers.Count; ++i)
            {
                if (Triggers[i].HandleTrigger(fsm))
                {
                    FSMStateID stateID = map[Triggers[i].TriggerID];
                    fsm.ChangeActiveState(stateID);
                    return;
                }
            }
        }

        public virtual void EnterState(FSMBase fsm) { }
        public virtual void ActionState(FSMBase fsm) { }
        public virtual void ExitState(FSMBase fsm) { }
    }

}
