using UnityEngine;
using System.Collections;

namespace GameLogic.Managers
{
    public class SpawnPointManager : MonoBehaviour
    {
        private GameObject[] spawnObj;
        private int nextObjIndex;

        public bool playAnimation = false;

        private void Awake()
        {
            spawnObj = new GameObject[transform.childCount];
            for(int i = 0; i < transform.childCount; i++)
            {
                spawnObj[i] = transform.GetChild(i).gameObject;
                spawnObj[i].SetActive(false);
            }
            nextObjIndex = 0;
        }

        //生成
        public void Spawn()
        {
            if(nextObjIndex < spawnObj.Length)
            {
                if (playAnimation)
                {
                    //todo:设置动画
                }
                else
                {
                    GenerateEntity();
                }

            }
        }

        public void GenerateEntity()
        {
            spawnObj[nextObjIndex].SetActive(true);
            spawnObj[nextObjIndex].transform.parent = transform.root;
            nextObjIndex++;
        }

        //重置出生点
        public void ResetSpawnPoint()
        {
            for(int i = 0; i < spawnObj.Length; i++)
            {
                spawnObj[i].transform.parent = transform;
                spawnObj[i].gameObject.SetActive(false);
            }
            nextObjIndex = 0;
        }
    }
}