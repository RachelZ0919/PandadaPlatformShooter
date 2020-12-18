using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.EntityStats;

namespace GameLogic.Item {

    public class HealthAid : MonoBehaviour, IItem
    {
        public void PickUp(Transform entity)
        {
            Debug.LogWarning("捡起了血包");
            Stats stat = entity.GetComponent<Stats>();


            // 如果满血，提升一点血上限
            if (stat.health == stat.maxHealth)
            {
                stat.SetValue("health", stat.maxHealth + 1);
                stat.SetValue("maxHealth", stat.maxHealth + 1);
            }
            // 不满血，加1血
            else
            {
                stat.SetValue("health", Mathf.Min(stat.health + 1, stat.maxHealth));
            }

            Destroy(gameObject);
        }
    }
}

