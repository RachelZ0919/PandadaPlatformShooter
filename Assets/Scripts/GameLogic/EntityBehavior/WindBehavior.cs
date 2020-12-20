using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityStats;

/// <summary>
/// 风场
/// </summary>
public class WindBehavior : MonoBehaviour
{
    [SerializeField] private StatData playerStat;
    [SerializeField] private GameObject player;

    private Stats stat;
    private Rigidbody2D rigidbody;
    private float playerSpeed;
    private float targetSpeed;
    private float speedStep;
    private float refvel;

    private AreaEffector2D areaEffector;

    private bool isWindOn = false;

    private void Awake()
    {
        areaEffector = GetComponent<AreaEffector2D>();
        rigidbody = player.GetComponent<Rigidbody2D>();
        stat = player.GetComponent<Stats>();
    }

    private void Start()
    {
        playerSpeed = playerStat.speed;
        targetSpeed = playerSpeed;
    }

    /// <summary>
    /// 保证角色在逆风时，不会逐渐加速到原有最大速度
    /// </summary>
    void Update()
    {

        if (isWindOn)
        {
            if ((areaEffector.forceAngle == 0 && rigidbody.velocity.x < 0) || (areaEffector.forceAngle == 180 && rigidbody.velocity.x > 0)) 
            {
                targetSpeed = playerSpeed / 3;
                speedStep = 0.1f;
            }
            else
            {
                targetSpeed = playerSpeed;
                speedStep = 0.1f;
            }
        }
        else
        {
            targetSpeed = playerSpeed;
            if ((areaEffector.forceAngle == 0 && rigidbody.velocity.x < 0) || (areaEffector.forceAngle == 180 && rigidbody.velocity.x > 0))
            {
                speedStep = 0.5f;
            }
        }

        stat.SetValue("speed", Mathf.SmoothDamp(stat.speed, targetSpeed, ref refvel, speedStep));
    }

    public void SetWind(AnimationEvent animationEvent)
    {
        bool b = animationEvent.intParameter > 0;
        isWindOn = b;
        areaEffector.enabled = b;
    }
}
