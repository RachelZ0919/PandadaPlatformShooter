﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace animation1
{
    public class pic1 : MonoBehaviour
{
    private float UI_Alpha = 1;             //初始化时让UI显示
    float alphaSpeed = 0.8f;          //渐隐渐显的速度
    private CanvasGroup canvasGroup;
    // Use this for initialization
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(strory1.end)
        // {
            ChangeColor();
        // }
        
    }


    void ChangeColor() 
    {
        if (canvasGroup == null)
        {
            Debug.Log("no pic canvas");
            return;
        }

        if (UI_Alpha != canvasGroup.alpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = UI_Alpha;
            }
        }
        // else
        // {
        //     //透明度降低
        //     UI_Alpha=0;
        //     canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, -10.0f*alphaSpeed * Time.deltaTime);
        //     if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
        //     {
        //         canvasGroup.alpha = UI_Alpha;
        //         end=true;
        //     }
        // }
    }
    public void UI_FadeIn_Event()
    {
        UI_Alpha = 1;
        canvasGroup.blocksRaycasts = true;      //可以和该对象交互
    }
    public void UI_FadeOut_Event()
    {
        UI_Alpha = 0;
        canvasGroup.blocksRaycasts = false;     //不可以和该对象交互
    }

    internal void ToHideThis() 
    {
        canvasGroup.alpha = 0f;
    }
}
}


