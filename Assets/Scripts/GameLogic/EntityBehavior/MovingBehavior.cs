using UnityEngine;
using System.Collections;

namespace GameLogic.EntityBehavior
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovingBehavior : MonoBehaviour
    {
        //todo:移动行为设置相关参数与函数（包含Editor）

        private Rigidbody2D rigidbody; //人物rigidbody2D组件
        private Animator animator; // 人物animator
        private float direction; // 人物移动方向
        private float lastDirection; //上一帧人物移动方向

        private float width; //角色宽度
        private float height; //角色高度

        [SerializeField] private float startAcc = 30; //起跑加速度
        [SerializeField] private float maxVel = 10; //最大速度
        [SerializeField] private float stopAcc = 50; //停止加速度
        [SerializeField] private float turnAcc = 55; //转向加速度

        [SerializeField] private float jumpForce = 10; //跳跃高度
        [SerializeField] private int jumpingBufferFrame = 3; //跳跃缓冲帧

        private bool isAccelerating;
        private bool isTurning;
        private bool turningEnd;
        private bool isStopping;

        private bool isOnGround;
        private int leavingGroundJumpingFrame; //离地后的跳跃缓冲
        private int fallingJumpingFrame; //落地前的跳跃缓冲


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Vector3 size = GetComponent<SpriteRenderer>().sprite.bounds.size;
            height = size.x;
            width = size.y;
        }

        private void Start()
        {
            direction = 0;
            isAccelerating = false;
            isTurning = false;
            isStopping = false;
            turningEnd = true;
            isOnGround = false;
            leavingGroundJumpingFrame = 0;
            fallingJumpingFrame = 0;
        }

        public void FixedUpdate()
        {
            float xSpeed = rigidbody.velocity.x;
            float ySpeed = rigidbody.velocity.y;
            
            //todo : 把加速度与速度关联，不再用clamp函数
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

            animator.SetFloat("xspeed", Mathf.Abs(xSpeed));
            animator.SetFloat("yspeed", ySpeed);
            animator.SetBool("isOnGround", isOnGround);
            rigidbody.velocity = new Vector2(xSpeed, ySpeed);
        }

        private void Update()
        {
            if (isOnGround)//检测是否在地面
            {
                int layerMask = LayerMask.GetMask("Scene");
                Vector3 positionOffset = new Vector3(width / 2, 0, 0);
                bool hitNothing = true;

                //左右各测一次是否接触地面，如果均没有接触，就说明没有在地面上。
                RaycastHit2D hit = Physics2D.Raycast(transform.position + positionOffset , Vector2.down, height/2 + 0.1f , layerMask);
                if (hit) hitNothing = false;
                hit = Physics2D.Raycast(transform.position - positionOffset, Vector2.down, height / 2 + 0.1f, layerMask);
                if (hit) hitNothing = false;
                
                if (hitNothing)
                {
                    isOnGround = false;
                    leavingGroundJumpingFrame = jumpingBufferFrame;//更新缓冲帧
                }

                if(fallingJumpingFrame > 0)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                    isOnGround = false;
                    fallingJumpingFrame = 0;
                }
            }
        }


        private void LateUpdate()
        {
            //todo : 换一种判断方法，太多bool了
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
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                isOnGround = false;
            }
            else
            {
                fallingJumpingFrame = jumpingBufferFrame;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                Vector2 normal = collision.contacts[0].normal;
                if(Vector2.Angle(normal, Vector2.up) < 1f)
                {
                    isOnGround = true;
                    leavingGroundJumpingFrame = 0;
                }
            }
        }
    }
}