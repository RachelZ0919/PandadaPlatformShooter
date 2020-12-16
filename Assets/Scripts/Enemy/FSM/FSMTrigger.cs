using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM{
    /// <summary>
    /// 条件类，切换状态的条件，所有条件的编号。
    /// </summary>
    public abstract class FSMTrigger
    {
        //编号
        public FSMTriggerID TriggerID { get; set; }

        //逻辑处理
        public FSMTrigger()
        {
            Init(); //必须初始化，构造函数不能写纯虚函数
        }

        public abstract void Init();

        public abstract bool HandleTrigger(FSMBase fsm);

    }

}