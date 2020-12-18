using UnityEngine;
using System.Collections;
using GameLogic.Item;
using GameLogic.EntityStats;

namespace GameLogic.EntityBehavior
{
    public class PickingBehavior : MonoBehaviour
    {
        [SerializeField] private bool pickUpStatItem = true;
        private IItem touchingItem;

        /// <summary>
        /// 拾取
        /// </summary>
        public void Pick()
        {
            if (touchingItem != null)
            {
                touchingItem.PickUp(transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 11)
            {
                IItem item = collision.collider.GetComponent<IItem>();
                item.PickUp(transform);
            }


            //todo:判断当前触碰物体
            //switch (collision.gameObject.tag)
            //{

            //    case "HealthUp":
            //        {
            //            Debug.LogWarning("捡起了血包");
            //            Stats stat = this.GetComponent<Stats>();
            //            if (collision.gameObject.name == "HealthUp")
            //            {

            //                // 如果满血，提升一点血上限
            //                if (stat.health == stat.maxHealth)
            //                {
            //                    stat.SetValue("health", stat.maxHealth + 1);
            //                }
            //                // 不满血，加1血
            //                else
            //                {
            //                    stat.SetValue("health", Mathf.Min(stat.health + 1, stat.maxHealth));
            //                }
            //            }
            //            else
            //            {
            //                // 提升两点血上限
            //                if (stat.health == stat.maxHealth)
            //                {
            //                    stat.SetValue("health", stat.maxHealth + 2);
            //                }
            //                // 不满血，加2血
            //                else
            //                {
            //                    stat.SetValue("health", stat.health + 2);
            //                }
            //            }
            //            Debug.LogWarning("加HP至" + stat.health);
            //            //摧毁物体 
            //            Destroy(collision.gameObject);
            //            break;
            //        }
            //    case "WeaponOnGround":
            //        {
            //            Debug.LogWarning("捡起了武器");
            //            //Stats stat = this.GetComponent<Stats>();
            //            //// 角色攻击力提升
            //            //stat.SetValue("attack", stat.attack + 1);
            //            //Debug.LogWarning("角色攻击力提升" + stat.attack);
            //            //Transform entity=
            //            Destroy(collision.gameObject);
            //            break;
            //        }
           // }
        }
    }
}