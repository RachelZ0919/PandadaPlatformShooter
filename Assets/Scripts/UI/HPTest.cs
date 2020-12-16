using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameLogic.EntityStats;


    public class HPTest : MonoBehaviour
    {
        public Stats stat;
        public Text text;
    private void Update()
    {
        text.text = $"{stat.health}";
    }
}
