using UnityEngine;
using CameraLogic;
using GameLogic.Managers;

namespace LevelEvents
{
    public class AnimationTool : MonoBehaviour
    {
        //public void UseScreenShake()
        //{
        //    CameraShake.instance.ShakeScreen(0.3f, 0.1f);
        //}

        public void UseScreenShake(AnimationEvent animationEvent)
        {
            float time = 1f / animationEvent.intParameter;
            CameraShake.instance.ShakeScreen(time, animationEvent.floatParameter);
        }

        public void BossShakeInTime(AnimationEvent animationEvent)
        {
            CameraShake.instance.ShakeScreen(animationEvent.floatParameter, 0.02f);
        }

        public void AnimationEnd()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void SpawnPlayer()
        {
            GameManager.instance.SpawnPlayer();
        }

        public void SetControl(AnimationEvent animationEvent)
        {
            GameManager.instance.AllowControl(animationEvent.intParameter > 0);
        }
    }
}