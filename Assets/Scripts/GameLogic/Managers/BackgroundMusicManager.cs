using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic.Managers
{
    public class BackgroundMusicManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level >= 12)
            {
                Destroy(gameObject);
            }
        }
    }
}