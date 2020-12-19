using UnityEngine;
using CameraLogic;
using GameLogic.Managers;

namespace LevelEvents
{
    public class AnimationTool : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

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

        public void ShowText(AnimationEvent animationEvent)
        {
            animator.SetBool("isTextCompleted", false);
            MiddleText text = GameObject.Find(animationEvent.stringParameter).GetComponent<MiddleText>();
            text.OnTextEnd += OnTextEnd;
            text.StartShowingText();
        }

        private void OnTextEnd(MiddleText text)
        {
            text.OnTextEnd -= OnTextEnd;
            animator.SetBool("isTextCompleted", true);
        }
    }
}