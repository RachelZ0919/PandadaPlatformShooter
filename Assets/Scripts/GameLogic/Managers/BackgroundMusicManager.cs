using UnityEngine;


namespace GameLogic.Managers
{
    public class BackgroundMusicManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if(GameManager.instance != null)
            {
                if (GameManager.instance.playerDead)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}