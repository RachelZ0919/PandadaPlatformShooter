using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UILogic
{
    public class ResolutionTest : MonoBehaviour
    {
        public Text text;
        private string initialRes;

        private void Awake()
        {
            initialRes = $"InitialRes:{Screen.width},{Screen.height}";
        }

        // Update is called once per frame
        void Update()
        {
            text.text = initialRes + $"\nCurrent Res:{Screen.width},{Screen.height}";
        }
    }
}