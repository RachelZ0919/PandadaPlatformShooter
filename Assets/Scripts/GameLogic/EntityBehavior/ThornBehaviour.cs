using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityStats;

/// <summary>
/// 角色碰到地刺将会死亡
/// </summary>
public class ThornBehaviour : MonoBehaviour
{
    public bool goDie;
    public float thoneDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Stats stat = collision.GetComponent<Stats>();
            if (goDie)
            { 
                stat.SetValue("health", 0);
                Debug.LogWarning("Player死亡");
            }
            else
            {
                if(thoneDamage >0)
                {
                    stat.SetValue("health", Mathf.Max(stat.health - thoneDamage, 0));
                    Debug.LogWarning("Player减少" + thoneDamage + "HP");
                }
                else
                {
                    Debug.LogWarning("伤害必须大于0");
                }
            }
        }
        
    }
}
