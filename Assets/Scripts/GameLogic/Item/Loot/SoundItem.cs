using UnityEngine;
using GameLogic.Managers;

namespace GameLogic.Item.Loot
{
    public class SoundItem : MonoBehaviour, IItem
    {
        [SerializeField] private AudioClip backgoundMusic;
        private AudioSource audio;

        private Rigidbody2D rigidbody;
        private SpriteRenderer spriteRenderer;
        private Collider2D collider;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audio = GetComponent<AudioSource>();
        }

        public void PickUp(Transform entity)
        {
            audio.loop = true;
            audio.clip = backgoundMusic;
            audio.Play();

            collider.enabled = false;
            rigidbody.simulated = false;
            spriteRenderer.enabled = false;

            transform.position = Vector3.zero;

            GameManager.instance.SpawnLeavePortal();
        }


    }
}