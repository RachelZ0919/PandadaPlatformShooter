using UnityEngine;
using System.Collections;

namespace GameLogic.Item
{
    public interface IItem
    {
        /// <summary>
        /// 物品被拾起后行为
        /// </summary>
        /// <param name="entity">拾起它的实体</param>
        void PickUp(Transform entity);
    }
}