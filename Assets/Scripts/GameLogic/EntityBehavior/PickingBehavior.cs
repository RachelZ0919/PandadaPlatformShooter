using UnityEngine;
using System.Collections;
using GameLogic.Item;

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
            if(touchingItem != null)
            {
                touchingItem.PickUp(transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //todo:判断当前触碰物体，以及其拾取逻辑
        }
    }
}