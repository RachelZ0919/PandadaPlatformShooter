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
        #region Variables

        #region State
        private List<FSMState> states;

        [Tooltip("默认状态")]
        public FSMStateID defaultStateID;        
        private FSMState currentState;
        public FSMState defaultState;

        //public FSMStateID test_CurrentStateID;
        [HideInInspector] public ShootingBehavior shootingBehavior;//射击行为
        [HideInInspector] public Stats chStatus; //人物状态
        [HideInInspector] public Animator anim;
        public ParticleSystem parEffet;

        #endregion

        #region Target Searching

        [Tooltip("攻击距离")]
        public float attackDistance;
        [Tooltip("视野距离")]
        public float sightDistance;
        [Tooltip("攻击目标标签")]
        public string[] targetTags = { "Player" };

        [HideInInspector] public Transform targetTF;



        public float runSpeed = 4f;
        public float walkSpeed = 2f;

        //控制播放攻击动画
        public float holdingTime = 0.42f;
        public float attackTime = 6f;
        public bool isAttacking = false;
        [HideInInspector] public float timer;

        [Tooltip("巡逻点")]
        public Transform[] wayPoints;
        public PatrolMode patrolMode;
        public bool isPatrolComplete;
        #endregion

        #region Moving
        /// <summary>
        /// 是否可以移动
        /// </summary>
        public bool canMove = true;
        //巡逻点索引
        private int index = 0;
        #endregion

        #endregion
        internal void MoveToTarget(Vector3 position, float attackDistance, object moveSpeed)
        {
            throw new NotImplementedException();
        }

        #region Initialize

        private void Awake()
        {
            InitComponent();
            ConfigFSM();
            
        }

        private void Start()
        {
            parEffet.Stop();
        }

        private void OnEnable()
        {
            InitDefaultDtate();
        }

        /// <summary>
        /// 设置初始状态
        /// </summary>
        private void InitDefaultDtate() 
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitComponent()
        {
            shootingBehavior = GetComponent<ShootingBehavior>();
            chStatus = GetComponent<Stats>();
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
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
            attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Patrolling);
            attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);
            states.Add(attacking);

            PatrollingState patrolling = new PatrollingState();
            patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
            patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
            patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);
            states.Add(patrolling);
        }

        #endregion


        #region State Machine


        //配置状态机，创建状态对象，设置状态
        private void Update()
        {
            //test_CurrentStateID = currentState.StateID;
            //判断当前状态并执行
            currentState.Reason(this);
            currentState.ActionState(this);
            SearchTarget();
        }
        
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateID">状态ID</param>
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

        #endregion

        #region Moving

        /// <summary>
        /// 移动到对应位置
        /// </summary>
        /// <param name="position">目标位置</param>
        /// <param name="stopDistance">停止位置</param>
        /// <param name="moveSpeed">移动速度</param>
        public void MoveToTarget(Vector3 position, float stopDistance, float moveSpeed)
        {
            if (wayPoints[index].GetComponent<Collider2D>().OverlapPoint(transform.position))
            {
                index = (index + 1) % wayPoints.Length;
            }
            if (Vector3.Distance(transform.position, position) > stopDistance && canMove)
            {
                Vector3 moveDirection = position - transform.position;
                transform.position = Vector3.MoveTowards(transform.position,
                    wayPoints[index].position, moveSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            canMove = false;
        }

        #endregion

        #region Target Search

        /// <summary>
        /// 寻找满足条件的目标
        /// </summary>
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

        #endregion

        //攻击动画帧事件
        public void Attack()
        {
            Vector3 direction = targetTF.position - transform.position;
            shootingBehavior.Shoot(direction);
            parEffet.Play();
        }

        public void StopParticle()
        {
            parEffet.Stop();
        }
    }
}
