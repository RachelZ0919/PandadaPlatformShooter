using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



namespace animation1
{
public class control_story3 : MonoBehaviour
{
    private float UI_Alpha = 1;             //初始化时让UI显示
    float alphaSpeed = 0.8f;          //渐隐渐显的速度
    private CanvasGroup canvasGroup;
    private CanvasScaler canvasScaler;
    Image computer;
    int count=0;
    bool second=false;//第二步，第二段加载
    bool third=false;//第三部，两段一起消失
    

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
            if(!second)
            {
                canvasGroup=GameObject.Find("story2_canvas2").GetComponent<CanvasGroup>();
                second=true;
            }
            else
            {
                //转场
                // Change_Scenes.sceneStarting=false;
                switchScene("EndScene");
            }
            // if(third)
            // {
            //     //透明度降低
            //     UI_Alpha=0;
            //     canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, -10.0f*alphaSpeed * Time.deltaTime);
            //     if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            //     {
            //         canvasGroup.alpha = UI_Alpha;
            //     }
            // }
            

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



