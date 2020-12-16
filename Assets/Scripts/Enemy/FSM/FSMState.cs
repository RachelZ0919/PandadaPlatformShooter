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
        public FSMStateID StateID { get; set; }

        private Dictionary<FSMTriggerID, FSMStateID> map;

        //条件列表
        private List<FSMTrigger> Triggers;

        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            Triggers = new List<FSMTrigger>();
            Init();
        }

        public abstract void Init();

        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            map.Add(triggerID, stateID);

            CreateTrigger(triggerID);
        }

        //创建条件对象,由状态机调用
        private void CreateTrigger(FSMTriggerID triggerID)
        {
            //命名规范：AI.FSM+条件枚举+Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            Triggers.Add(trigger);
        }

        //检测当前状态的条件是否满足
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
