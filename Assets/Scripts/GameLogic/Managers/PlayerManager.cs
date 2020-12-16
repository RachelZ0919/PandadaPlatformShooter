using UnityEngine;
using System.Collections;
using GameLogic.EntityStats;
using GameLogic.EntityBehavior;
using GameLogic.Item.Weapon;

namespace GameLogic.Managers
{
    [RequireComponent(typeof(Rigidbody2D))]
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
            ShootingBehavior shootingBehavior = GetComponent<ShootingBehavior>();
            MovingBehavior movingBehavior = GetComponent<MovingBehavior>();
            HitBehavior hitBehavior = GetComponent<HitBehavior>();

            //初始化枪
            if(shootingBehavior != null)
            {
                Weapon weapon = Instantiate(statData.defaultWeapon).GetComponent<Weapon>();
                weapon.PickUp(transform);
                shootingBehavior.audio = usingAudio;
            }

            if(movingBehavior != null)
            {
                movingBehavior.audio = usingAudio;
            }

            if (hitBehavior != null)
            {
                hitBehavior.audio = usingAudio;
            }
        }
    }
}