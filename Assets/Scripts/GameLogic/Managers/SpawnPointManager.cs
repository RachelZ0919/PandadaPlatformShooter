using UnityEngine;
using System.Collections;

namespace GameLogic.Managers
{
    public class SpawnPointManager : MonoBehaviour
    {
        private struct OriginTransform
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 localscale;

            public OriginTransform(Transform transform)
            {
                position = transform.position;
                rotation = transform.rotation;
                localscale = transform.localScale;
            }
        }

        private GameObject[] spawnObj;
        private OriginTransform[] originTransforms;
        private int nextObjIndex;

        public bool playAnimation = false;

        private void Awake()
        {
            spawnObj = new GameObject[transform.childCount];
            originTransforms = new OriginTransform[transform.childCount];
            for(int i = 0; i < transform.childCount; i++)
            {
                spawnObj[i] = transform.GetChild(i).gameObject;
                OriginTransform origin = new OriginTransform(spawnObj[i].transform);
                originTransforms[i] = origin;
                spawnObj[i].SetActive(false);
            }
            nextObjIndex = 0;
        }

        private void Start()
        {
            
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
                spawnObj[i].transform.position = originTransforms[i].position;
                spawnObj[i].transform.rotation = originTransforms[i].rotation;
                spawnObj[i].transform.localScale = originTransforms[i].localscale;

                spawnObj[i].gameObject.SetActive(false);
            }
            nextObjIndex = 0;
        }
    }
}