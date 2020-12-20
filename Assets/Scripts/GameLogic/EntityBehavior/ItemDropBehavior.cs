using UnityEngine;
using GameLogic.Managers;

namespace GameLogic.EntityBehavior
{
    public class ItemDropBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject itemToDrop;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Vector2 spawnDirection = Vector2.up;

        private void Start()
        {
            GetComponent<EntityManager>().OnObjectVisuallyDeath += DropItem;
        }

        public void DropItem(EntityManager entity)
        {
            if (spawnPoint == null) spawnPoint = transform;
            GameObject item = Instantiate(itemToDrop, spawnPoint.position, Quaternion.identity);
            FallingBehavior fallingBehavior = item.GetComponent<FallingBehavior>();
            if(fallingBehavior == null)
            {
                Debug.LogWarning("the item does not have a falling behavior");
            }
            else
            {
                fallingBehavior.Initialize(spawnDirection.normalized * 5);
            }
        }
    }
}