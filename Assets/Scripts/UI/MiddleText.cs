﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 消息框
/// </summary>
public class MiddleText : MonoBehaviour
{
    /// <summary>
    /// 每个字的间隔
    /// </summary>
    public float letterPause = 0.1f;
    /// <summary>
    /// 要显示的字
    /// </summary>
    public string[] lines;


    private float lastTime = 0;
    private string totalWords;
    private int currentLineIndex;
    private int currentWordIndex;

    private Vector2 bubbleSize;

    private bool hasStartShowingText;

    [SerializeField] private Text text;
    [SerializeField] private RectTransform bubble;

    void Start()
    {
        text.text = "";
        currentLineIndex = 0;
        currentWordIndex = 0;

        hasStartShowingText = false;

        bubbleSize = bubble.sizeDelta;
        Debug.Log(bubbleSize);
        
        bubble.gameObject.SetActive(false);


    }

    // 开启消息框
    public void StartShowingText()
    {
        if(lines.Length > 0)
        {
            bubble.gameObject.SetActive(true);
            hasStartShowingText = true;
            currentLineIndex = 0;
            totalWords = lines[currentLineIndex++];
            ResetBubble();
        }
    }

    private void ResetBubble()
    {
        bubble.sizeDelta = bubbleSize;
    }

    private void Update()
    {
        if (hasStartShowingText)
        {
            if (Time.time - lastTime > letterPause)
            {
                if(totalWords.Length > currentWordIndex)
                {
                    text.text += totalWords[currentWordIndex++];
                    if(currentWordIndex / 22 < 0)
                    {
                        bubble.sizeDelta = new Vector2(bubbleSize.x + (currentWordIndex - 1) * 33, bubbleSize.y);
                    }
                    
                    lastTime = Time.time;
                }
                else
                {
                    if(lines.Length > currentLineIndex)
                    {
                        currentWordIndex = 0;
                        totalWords = lines[currentLineIndex++];
                        text.text = "";
                    }
                    else
                    {
                        hasStartShowingText = false;
                        //todo: tell other finished
                    }

                }

            }
        }
    }

}