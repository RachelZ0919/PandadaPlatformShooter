using UnityEngine;
using GameLogic.Item.Weapon;
using GameLogic.Managers;

namespace GameLogic.Item
{
    //屑代码
    public class MaterialItem : MonoBehaviour, IItem
    {
        public Material material;
        public Sprite gunSprite;

        private bool isPickedUp = false;
        private float startTime;


        public void PickUp(Transform entity)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            entity.Find("body").GetComponent<SpriteRenderer>().material = material;
            entity.Find("head").GetComponent<SpriteRenderer>().material = material;
            entity.GetComponentInChildren<Weapon.Weapon>().GetComponent<SpriteRenderer>().sprite = gunSprite;

            startTime = Time.time + 0.1f;
            isPickedUp = true;
        }

        private void Update()
        {
            if (isPickedUp)
            {
                float value = Mathf.Abs(Time.time - startTime) * 1.5f;
                material.SetFloat("_AnimationTime", value);
                if(value >= 2)
                {
                    GameManager.instance.SpawnLeavePortal();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}