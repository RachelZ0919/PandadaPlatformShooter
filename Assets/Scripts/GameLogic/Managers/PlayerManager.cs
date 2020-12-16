using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using GameLogic.Item.Weapon;

namespace GameLogic.Managers
{
    [RequireComponent(typeof(MovingBehavior),typeof(ShootingBehavior),typeof(Rigidbody2D))]
    [RequireComponent(typeof(Stats),typeof(Animator),typeof(SpriteRenderer))]
    /// <summary>
    /// 负责管理玩家属性
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private StatData statData;
        [SerializeField] private AudioManager usingAudio;

        private void Start()
        {
            //初始化角色属性
            GetComponent<Stats>().InitializeStats(statData);
            //初始化枪
            Weapon weapon = Instantiate(statData.defaultWeapon).GetComponent<Weapon>();
            weapon.PickUp(transform);

            if(usingAudio == null)
            {
                //todo : 使用默认的
            }
            else
            {
                GetComponent<ShootingBehavior>().audio = usingAudio;
                GetComponent<MovingBehavior>().audio = usingAudio;
            }
        }
    }
}