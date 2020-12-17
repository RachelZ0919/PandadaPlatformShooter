using UnityEngine;
using System.Collections;

namespace GameLogic.Managers
{
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance;

        private void Awake()
        {
            if(instance != null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void CheckDeath(GameObject gameObject)
        {
            if (gameObject.CompareTag("player"))
            {
                //todo:游戏结束
            }
        }

        public void OnObjectCreate(EntityManager obj)
        {
            obj.OnObjectDeath += CheckDeath;
        }
    }
}