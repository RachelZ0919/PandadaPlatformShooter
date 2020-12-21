using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



namespace animation1
{
public class control_story2 : MonoBehaviour
{
     private float UI_Alpha = 1;             //初始化时让UI显示
    float alphaSpeed = 0.8f;          //渐隐渐显的速度
    private CanvasGroup canvasGroup;
    private CanvasScaler canvasScaler;
    Image computer;
    int count=0;

    // Use this for initialization
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }


    void ChangeColor() 
    {
        if (canvasGroup == null)
        {
            Debug.Log("no canvas");
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
        else
        {
            //透明度降低
            UI_Alpha=0;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, -10.0f*alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = UI_Alpha;
                //显示屏上移，相机缩放
                // if(count<=120)
                // {
                //     Vector3 offset = new Vector3(0,1,0);
                //     computer.transform.position+=offset;
                //     count++;
                // }
               
                // if(canvasScaler.scaleFactor-5.0f<=0.01f&&count>120)
                // {
                //     canvasScaler.scaleFactor+=0.01f;
                // }
               
                    //切换场景
                    switchScene("Level-1-Start");
                
                
                

            }
        }
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

     public void switchScene(string sceneName)
        {
            StartCoroutine(Load(sceneName));
        }

        private IEnumerator Load(string sceneName)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitForEndOfFrame();
            op.allowSceneActivation = true;
        }
  
}
}



