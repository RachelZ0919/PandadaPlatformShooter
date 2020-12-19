using UnityEngine;
using System.Collections;

namespace GameLogic.Managers
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance;
        private int enemyCount = 0;
        private SpawnPointManager[] spawnPoints;
        private SpawnPointManager playerSpawn;
        [SerializeField] private GameObject canvas; 

        [SerializeField] private bool playerSpawnImmediately = true;
        [SerializeField] private bool allowControlAtStart = true;


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

            if (gameObject.CompareTag("Player"))
            {
                //todo:游戏结束
            }else if (obj.CompareTag("enemy"))
            {
                enemyCount--;
                if(enemyCount == 0)
                {
                    //todo:进入下一关
                }
            }
        }

        public void OnObjectCreate(EntityManager obj)
        {
            obj.OnObjectDeath += CheckDeath;
            if (obj.CompareTag("enemy"))
            {
                enemyCount++;
            }
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        public void GameStart()
        {
            if (playerSpawnImmediately && playerSpawn != null) playerSpawn.Spawn();
            canvas.SetActive(allowControlAtStart);
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].Spawn();
            }
        }

        /// <summary>
        /// 游戏重置
        /// </summary>
        public void GameReset()
        {
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].ResetSpawnPoint();
            }
        }

        public void SpawnPlayer()
        {
            if (playerSpawn != null) playerSpawn.Spawn();
            else Debug.LogWarning("no player spawn!");
        }

        public void AllowControl(bool b)
        {
            canvas.SetActive(b);
        }

    }
}