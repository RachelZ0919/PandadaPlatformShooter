using UnityEngine;
using GameLogic.Managers;

namespace GameLogic.EntityBehavior
{
    public class LandDetector : MonoBehaviour
    {
        [HideInInspector] public AudioManager audio;
        [HideInInspector] public MovingBehavior mov;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & (1 << 8 | 1 << 10)) > 0)
            {
                if(!mov.isOnGround && audio != null)
                {
                    audio.PlayAudio("landAudio");
                }
            }
        }
    }
}