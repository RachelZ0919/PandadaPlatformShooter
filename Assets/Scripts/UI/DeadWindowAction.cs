using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameLogic.EntityStats;

/// <summary>
/// 开启死亡结算界面
/// </summary>
public class DeadWindowAction : MonoBehaviour
{
    public GameObject canvas;
    private bool hasStartShowingDeadWindow;

    void Start()
    {
 
    }

    
    public void OpenDeadWindow()
    {
        hasStartShowingDeadWindow = true;
    }

    void Update()
    {
        if(hasStartShowingDeadWindow)
        {
            canvas.SetActive(true);
        }
    }
}
