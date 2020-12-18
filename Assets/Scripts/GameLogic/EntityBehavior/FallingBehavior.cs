using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品掉落
/// </summary>
public class FallingBehavior : MonoBehaviour
{
    public float timer = 0.5f;

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
        material.bounciness = 0.4f;
        material.friction = 0;
        collider.sharedMaterial = material;
        //rigidbody.AddForce(new Vector2(50, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollider)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                collider.enabled = true;
                hasCollider = true;
            }
        }   
    }
}
