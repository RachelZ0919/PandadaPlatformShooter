using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaseFramework.Network
{

public class quit_game : MonoBehaviour
{
    Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        QuitButton=this.GetComponent<Button>();
        QuitButton.onClick.AddListener(quitgame);
    }
    void quitgame()
    {
        // LoginRequist.ucl.Close();
        // LoginRequist.ucl.Dispose();

//         #if UNITY_EDITOR

//         UnityEditor.EditorApplication.isPlaying = false;//用于退出运行

//          #else

        Application.Quit();

        // #endifs


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
