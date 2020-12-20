using UnityEngine;
using System.Collections;
using GameLogic.Managers;

namespace GameLogic.EntityBehavior
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovingBehavior : MonoBehaviour
    {
        #region Basic
        
        private Rigidbody2D rigidbody; //人物rigidbody2D组件

        private Vector3 hitBoxOffset; //碰撞盒位置
        private float width; //角色宽度
        private float height; //角色高度

        //判断当前状态用
        private bool isAccelerating;
        private bool isTurning;
        private bool turningEnd;
        private bool isStopping;
        private bool isOnGround;

        #endregion

        #region Walk

        public float lastNotZeroDirection;

        private float direction; // 人物移动方向
        private float lastDirection; //上一帧人物移动方向
        private float maxVel; //最大速度

        [SerializeField] private float startAcc = 50; //起跑加速度
        [SerializeField] private float stopAcc = 80; //停止加速度
        [SerializeField] private float turnAcc = 90; //转向加速度

        #endregion

        #region Jump

        private int leavingGroundJumpingFrame; //离地后的跳跃缓冲
        private int fallingJumpingFrame; //落地前的跳跃缓冲

        [SerializeField] private float jumpForce = 25; //跳跃高度
        [SerializeField] private int jumpingBufferFrame = 3; //跳跃缓冲帧

        #endregion

        #region Visual and Audio

        //动画
        private Animator animator; // 人物animator
        [SerializeField] private Transform movingVisualTransform;
        private Vector3 originScale;
        private Vector3 reverseScale;

        //音效
        public bool enableAudio = false;
        [HideInInspector] public AudioManager audio;


        #endregion

        private void Awake()
        {
            //加载组件
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            //记录碰撞体参数
            Vector3 size = GetComponent<Collider2D>().bounds.size;
            height = size.y;
            width = size.x;
            hitBoxOffset = GetComponent<Collider2D>().offset;

            //设置状态回调
            GetComponent<EntityStats.Stats>().OnStatsChanged += GetSpeed;
            
            //设置视觉用参数
            if (movingVisualTransform != null)
            {
                originScale = movingVisualTransform.localScale;
                originScale.x = Mathf.Abs(originScale.x);
                reverseScale = originScale;
                reverseScale.x *= -1;
            }
        }

        private void Start()
        {
            Initialize();
        }


        /// <summary>
        /// 行为初始化
        /// </summary>
        public void Initialize()
        {
            direction = 0;
            isAccelerating = false;
            isTurning = false;
            isStopping = false;
            turningEnd = true;
            isOnGround = false;
            leavingGroundJumpingFrame = 0;
            fallingJumpingFrame = 0;

            rigidbody.velocity = Vector2.zero;
        }

        public void FixedUpdate()
        {
            float xSpeed = rigidbody.velocity.x;
            float ySpeed = rigidbody.velocity.y;
            
            if (isAccelerating)//人物正常移动
            {
                xSpeed += lastDirection * startAcc * Time.deltaTime;
                xSpeed = Mathf.Clamp(xSpeed, -maxVel, maxVel);
            }else if (isStopping && xSpeed != 0)//人物在停止
            {
                float newSpeed =xSpeed - xSpeed / Mathf.Abs(xSpeed) * stopAcc * Time.deltaTime;
                if(newSpeed * xSpeed <= 0)
                {
                    xSpeed = 0;
                }
                else
                {
                    xSpeed = newSpeed;
                }
            }else if (isTurning)//人物在转向
            {
                xSpeed += lastDirection * turnAcc * Time.deltaTime;
                xSpeed = Mathf.Clamp(xSpeed, -maxVel, maxVel);
                if(Mathf.Abs(xSpeed) < 0.01f)
                {
                    turningEnd = true;
                }
            }

            //todo:跳跃滞空修正
            if (!isOnGround)
            {
                
            }

            //修改人方向
            if(movingVisualTransform != null)
            {
                if (xSpeed > 0)
                {
                    movingVisualTransform.localScale = originScale;
                }
                else if (xSpeed < 0)
                {
                    movingVisualTransform.localScale = reverseScale;
                }
            }

            animator.SetFloat("xspeed", Mathf.Abs(xSpeed));
            animator.SetFloat("yspeed", ySpeed);
            animator.SetBool("isOnGround", isOnGround);
            rigidbody.velocity = new Vector2(xSpeed, ySpeed);

            
        }

        private void Update()
        {
            if (isOnGround)//检测是否在地面
            {
                int layerMask = 1<<8 | 1<<9 | 1<<10;
                layerMask &= ~(1 << gameObject.layer);
                Vector3 positionOffset = new Vector3(width / 2, 0, 0) + hitBoxOffset;
                bool hitNothing = true;

                //左右中各测一次是否接触地面，如果均没有接触，就说明没有在地面上。
                RaycastHit2D hit = Physics2D.Raycast(transform.position + positionOffset , Vector2.down, height/2 + 0.1f , layerMask);
                if (hit) hitNothing = false;
                hit = Physics2D.Raycast(transform.position - positionOffset, Vector2.down, height / 2 + 0.1f, layerMask);
                if (hit) hitNothing = false;
                hit = Physics2D.Raycast(transform.position, Vector2.down, height / 2 + 0.1f, layerMask);
                if (hit) hitNothing = false;
                
                if (hitNothing)
                {
                    isOnGround = false;
                    leavingGroundJumpingFrame = jumpingBufferFrame;//更新缓冲帧
                }

                if(fallingJumpingFrame > 0)
                {
                    ExecuteJump();
                }
            }
        }

        private void LateUpdate()
        {
            //判断下一帧状态
            if (direction == 0)
            {
                isStopping = true;
                turningEnd = true;
                isAccelerating = isTurning = false;
            }
            else if (lastDirection * direction < 0)
            {
                isTurning = true;
                turningEnd = false;
                isAccelerating = isStopping = false;
            }
            else if (turningEnd) 
            {
                isAccelerating = true;
                isTurning = isStopping = false;
            }
            
            //更新方向
            if(Mathf.Abs(direction) > 0.3f)
            {
                lastNotZeroDirection = direction / Mathf.Abs(direction);
            }
            lastDirection = direction;
            direction = 0;

            //减少跳跃缓冲帧
            leavingGroundJumpingFrame = Mathf.Max(0, leavingGroundJumpingFrame - 1);
            fallingJumpingFrame = Mathf.Max(0, fallingJumpingFrame - 1);
        }


        /// <summary>
        /// 让人物按方向移动
        /// </summary>
        /// <param name="direction"> 人物移动方向，取值范围（-1，1） </param>
        public void MoveInDirection(float direction)
        {
            this.direction = Mathf.Clamp(direction, -1, 1);
        }

        /// <summary>
        /// 让人物跳跃
        /// </summary>
        public void Jump()
        {
            if (isOnGround || leavingGroundJumpingFrame > 0)
            {
                ExecuteJump();
            }
            else
            {
                fallingJumpingFrame = jumpingBufferFrame;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & (1 << 8 | 1 << 10)) > 0) 
            {
                Vector2 normal = collision.contacts[0].normal;
                if(Vector2.Angle(normal, Vector2.up) < 1f)
                {
                    isOnGround = true;
                    leavingGroundJumpingFrame = 0;
                }
            }
        }

        //获得速度
        private void GetSpeed(EntityStats.Stats stats)
        {
            maxVel = stats.speed;
        }

        /// <summary>
        /// 执行跳跃
        /// </summary>
        private void ExecuteJump()
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            if(enableAudio && audio != null) audio.PlayAudio("jumpAudio");
            isOnGround = false;
            fallingJumpingFrame = 0;
            leavingGroundJumpingFrame = 0;
        }


    }
}