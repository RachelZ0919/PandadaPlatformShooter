using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;
using GameLogic.EntityBehavior;
using GameLogic.EntityStats;
using GameLogic.Item.Weapon;

namespace AI.FSM
{
    /// <summary>
    /// 状态机Manager
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        private List<FSMState> states;

        [Tooltip("默认状态")]
        public FSMStateID defaultStateID;        
        private FSMState currentState;

        [HideInInspector] public MovingBehavior movingBehavior;
        [HideInInspector] public ShootingBehavior shootingBehavior;

        public float attackDistance;
        public float sightDistance;

        internal void MoveToTarget(Vector3 position, float attackDistance, object moveSpeed)
        {
            throw new NotImplementedException();
        }

        private FSMState defaultState;

        //初始化状态
        private void InitDefaultDtate()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        //配置状态机，创建对象，设置状态
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            IdleState idle = new IdleState();
            states.Add(idle);
            DeadState dead = new DeadState();
            states.Add(dead);     

            idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);

            PursuitState pursuit = new PursuitState();
            pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            pursuit.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
            pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);
            states.Add(pursuit);

            AttackingState attacking = new AttackingState();
            attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
            attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);
            states.Add(attacking);

            PatrollingState patrolling = new PatrollingState();
            patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
            patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);
            states.Add(patrolling);
        }

        private void Awake()
        {
            InitComponent();
        }

        private void Start()
        {
            ConfigFSM();
            InitDefaultDtate();
        }

        public FSMStateID test_CurrentStateID;

        //配置状态机，创建状态对象，设置状态
        private void Update()
        {
            test_CurrentStateID = currentState.StateID;
            //判断当前状态并执行
            currentState.Reason(this);
            currentState.ActionState(this);
            SearchTarget();
        }
        
        //切换状态
        public void ChangeActiveState(FSMStateID stateID)
        {
            currentState.ExitState(this); //离开上一个状态

            if (stateID == FSMStateID.Default)
            {
                currentState = defaultState;
            }
            else
            {
                currentState = states.Find(s => s.StateID == stateID);
            }

            currentState.EnterState(this);
        }

        [HideInInspector] //再面板不显示
        public Animator anim;
        [HideInInspector]
        public Stats chStatus; //人物状态
        public void InitComponent()
        {
            //anim = GetComponentInChildren<Animator>();
            movingBehavior = GetComponent<MovingBehavior>();
            shootingBehavior = GetComponent<ShootingBehavior>();
            chStatus = GetComponent<Stats>();
        }


        #region 寻找攻击目标
        //[HideInInspector]
        public Transform targetTF;

        [Tooltip("攻击目标标签")]
        public string[] targetTags = { "Player" };

        public float runSpeed = 4f;
        public float walkSpeed = 2f;
        [Tooltip("巡逻点")]
        public Transform[] wayPoints;
        public PatrolMode patrolMode;
        public bool isPatrolComplete;

        //寻找满足条件的目标
        private void SearchTarget()
        {
            List<Transform> targetArr = new List<Transform>();
            //添加所有Player进链表
            for (int i = 0; i < targetTags.Length; ++i)
            {
                GameObject[] tempGoArrat = GameObject.FindGameObjectsWithTag(targetTags[i]);
                targetArr.AddRange(tempGoArrat.Select(g => g.transform));
            }

            //找到第一个距离小于5的player。
            targetArr = targetArr.FindAll(t => Vector3.Distance(t.position, this.transform.position) <= this.sightDistance);
            Transform[] result= targetArr.ToArray();
            if (result.Length == 0)
            {
                targetTF = null;
            }
            else
            {
                targetTF = result[0];
            }
        }

        //移动
        public bool canMove = true;
        public void MoveToTarget(Vector3 position, float stopDistance, float moveSpeed)
        {
            if (Vector3.Distance(transform.position, position) > stopDistance && canMove)
            {
                float step = moveSpeed * Time.deltaTime;
                Vector3 moveDirection = position - transform.position;
                movingBehavior.MoveInDirection(moveDirection.x);
                //transform.position = Vector3.MoveTowards(transform.position, position, step);
            }
        }

        public void StopMove()
        {
            canMove = false;
        }


        #endregion

    }
}
