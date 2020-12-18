using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机敌人搜敌
/// </summary>
public class CameraSearchTarget : MonoBehaviour
{
    public Collider2D target;
    public bool canAttack = false;
    public bool canChangePosition = true; //攻击坐标，完成一次攻击后可更改
    public Vector3 targetPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other;
            canAttack = true;
            if (canChangePosition)
            {
                targetPosition = target.transform.position;
                //Debug.Log(targetPosition);
                canChangePosition = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!target && other.tag == "Player") 
        {
            target = other;
            canAttack = true;
            if (canChangePosition)
            {
                targetPosition = target.transform.position;
                //Debug.Log(targetPosition);
                canChangePosition = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == target)
        {
            target = null;
            //canAttack = false;
        }
    }
}
