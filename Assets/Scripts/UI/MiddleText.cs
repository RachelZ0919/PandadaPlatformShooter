using System.Collections;
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
    /// 每行的间隔
    /// </summary>
    public float linePause = 1.5f;
    /// <summary>
    /// 要显示的字
    /// </summary>
    public string[] lines;

    public delegate void TextEnd(MiddleText text);
    public TextEnd OnTextEnd;

    private float lastTime = 0;
    private float endLineTime;
    private bool lineEnd = false;
    private string totalWords;
    private int currentLineIndex;
    private int currentWordIndex;

    private Vector2 bubbleSize;

    private bool hasStartShowingText;

    [SerializeField] private Text text;
    [SerializeField] private RectTransform bubble;
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        text.text = "";
        currentLineIndex = 0;
        currentWordIndex = 0;

        hasStartShowingText = false;

        bubbleSize = bubble.sizeDelta;
        
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
            audio.Play();
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
            if (lineEnd)
            {
                if(Time.time - endLineTime > linePause)
                {
                    if(lines.Length <= currentLineIndex)
                    {
                        hasStartShowingText = false;
                        bubble.gameObject.SetActive(false);
                        OnTextEnd(this);

                    }
                    else
                    {
                        currentWordIndex = 0;
                        totalWords = lines[currentLineIndex++];
                        text.text = "";
                        text.text += totalWords[currentWordIndex++];
                        ResetBubble();
                        audio.Play();
                        lineEnd = false;
                    }


                }
            }
            else
            {
                if (Time.time - lastTime > letterPause)
                {
                    if (totalWords.Length > currentWordIndex)
                    {
                        text.text += totalWords[currentWordIndex++];
                        audio.Play();

                        if (currentWordIndex / 22 < 1)
                        {
                            bubble.sizeDelta = new Vector2(bubbleSize.x + (currentWordIndex - 1) * 35f, bubbleSize.y);
                        }
                        else
                        {
                            bubble.sizeDelta = new Vector2(bubbleSize.x + 21 * 35f, bubbleSize.y + (currentWordIndex / 22) * 31.5f);
                        }

                        lastTime = Time.time;
                    }
                    else
                    {
                        Debug.Log(currentLineIndex);
                        lineEnd = true;
                        endLineTime = Time.time;
                    }

                }
            }
        }
    }
}
