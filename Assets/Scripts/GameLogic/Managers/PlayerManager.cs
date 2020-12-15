using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using GameLogic.Item.Weapon;

namespace GameLogic.Managers
{
    /// <summary>
    /// 负责管理玩家属性
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private StatData statData;

        private void Start()
        {
            //初始化角色属性
            GetComponent<Stats>().InitializeStats(statData);
            //初始化枪
            Weapon weapon = Instantiate(statData.defaultWeapon).GetComponent<Weapon>();
            weapon.PickUp(transform);
        }
    }
}