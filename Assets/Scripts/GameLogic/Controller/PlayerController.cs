using UnityEngine;
using System.Collections;
using GameLogic.EntityBehavior;

namespace GameLogic.Controller
{
    /// <summary>
    /// PlayerController负责控制人物移动、射击
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region UIInterface
        
        [SerializeField] public Joystick movingJoystick;  //左摇杆，用于人物移动
        [SerializeField] public Joystick shootingJoystick; //右摇杆，用于人物射击

        #endregion

        #region Behavior

        private MovingBehavior movingBehavior; //移动逻辑
        private ShootingBehavior shootingBehavior; //射击逻辑

        #endregion

        private void Awake()
        {
            movingBehavior = GetComponent<MovingBehavior>() ;
            shootingBehavior = GetComponent<ShootingBehavior>();
        }

        private void Update()
        {
            movingBehavior.MoveInDirection(movingJoystick.Direction.x);

            if (Input.GetKey(KeyCode.A))
            {
                movingBehavior.MoveInDirection(-1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movingBehavior.MoveInDirection(1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movingBehavior.Jump();
            }

            Vector2 shootingDirection = shootingJoystick.Direction;
            if(shootingDirection.magnitude > 0.5f)
            {
                shootingBehavior.Shoot(shootingDirection);
            }

            Debug.Log(movingJoystick.Direction);

            if (movingJoystick.Direction.y > 0.7f) 
            {
                Debug.Log("jump");
                movingBehavior.Jump();
            }
        }
    }
}