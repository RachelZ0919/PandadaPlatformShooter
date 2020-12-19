using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品掉落
/// </summary>
public class FallingBehavior : MonoBehaviour
{
    [SerializeField] private float timer = 0.5f;
    private float currentTimer;

    PhysicsMaterial2D material;
    private Collider2D collider;

    // 是否弹起过
    private bool isJump = false;
    private bool hasCollider = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isJump)
        {
            material.bounciness = 0;
            collider.sharedMaterial = material;
            isJump = true;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<Collider2D>();
        material = new PhysicsMaterial2D();
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        //关闭碰撞体
        collider.enabled = false;

        //设置碰撞体材质
        material.bounciness = 0.4f;
        material.friction = 0;
        collider.sharedMaterial = material;

        //设置布尔变量
        isJump = false;
        hasCollider = false;

        //重设timer
        currentTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollider)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            else
            {
                collider.enabled = true;
                hasCollider = true;
            }
        }   
    }
}
