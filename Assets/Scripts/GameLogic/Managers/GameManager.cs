using UnityEngine;
using CameraLogic;
using UnityEngine.SceneManagement;

namespace GameLogic.Managers
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance;
        private int enemyCount = 0;
        private SpawnPointManager[] spawnPoints;
        private SpawnPointManager playerSpawn;
        [SerializeField] private GameObject controlUI;
        [SerializeField] private GameObject deathUI;

        [SerializeField] private bool playerSpawnImmediately = true;
        [SerializeField] private bool allowControlAtStart = true;

        private bool gameHasStart = false;
        private bool gameHasEnd = false;



        private void Awake()
        { 
            if (instance == null)
            {
                instance = this;

                //获取所有敌人Spawn Point
                GameObject[] spawnPointObj = GameObject.FindGameObjectsWithTag("enemy_spawn");
                spawnPoints = new SpawnPointManager[spawnPointObj.Length];
                for (int i = 0; i < spawnPointObj.Length; i++)
                {
                    spawnPoints[i] = spawnPointObj[i].GetComponent<SpawnPointManager>();
                }

                //获取玩家SpawnPoint
                GameObject playerSpawnObj = GameObject.FindGameObjectWithTag("player_spawn");
                if(playerSpawnObj != null)
                {
                    playerSpawn = playerSpawnObj.GetComponent<SpawnPointManager>();
                }
                else
                {
                    Debug.LogWarning("This scene doesn't have player!");
                }

                deathUI.SetActive(false);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            GameStart();
        }

        /// <summary>
        /// 实体死亡时回调，判断游戏胜利失败
        /// </summary>
        /// <param name="obj">实体</param>
        public void CheckDeath(EntityManager obj)
        {
            obj.OnObjectDeath -= CheckDeath; //删除回调函数

            Debug.Log(obj.name);

            if (obj.CompareTag("Player"))
            {
                Debug.Log("player dead");
                controlUI.SetActive(false);
                deathUI.SetActive(true);
                gameHasEnd = true;
            }else if (obj.CompareTag("enemy"))
            {
                enemyCount--;
                if(enemyCount == 0)
                {
                    Debug.Log("Player Win!");
                    //todo:进入下一关
                }
            }
        }


        private void Update()
        {
            if (gameHasEnd)
            {
                if (!CameraShake.instance.isShaking)
                {
                    Time.timeScale = 0;
                }
            }
        }

        public void OnObjectCreate(EntityManager obj)
        {
            if (gameHasStart)
            {
                obj.OnObjectDeath += CheckDeath;
                if (obj.CompareTag("enemy"))
                {
                    enemyCount++;
                }
            }
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        public void GameStart()
        {
            gameHasStart = true;
            if (playerSpawnImmediately && playerSpawn != null) playerSpawn.Spawn();
            controlUI.SetActive(allowControlAtStart);
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].Spawn();
            }
        }





        /// <summary>
        /// 游戏重置
        /// </summary>
        public void SpawnReset()
        {
            playerSpawn.ResetSpawnPoint();
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].ResetSpawnPoint();
            }
        }

        public void ReplayGame()
        {
            Debug.Log("replay");
        }

        public void GoToLevel(int level)
        {
            Time.timeScale = 1;
            gameHasEnd = false;
            SceneManager.LoadScene(level);
        }



        public void SpawnPlayer()
        {
            if (playerSpawn != null) playerSpawn.Spawn();
            else Debug.LogWarning("no player spawn!");
        }

        public void AllowControl(bool b)
        {
            controlUI.SetActive(b);
        }

    }
}