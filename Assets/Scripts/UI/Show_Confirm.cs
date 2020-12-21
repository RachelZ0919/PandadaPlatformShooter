using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Show_Confirm : MonoBehaviour
{
    Button button,b1,b2,b3,b4;
    Image i1,i2,i3,i4,i5;
    Text t1,t2,t3,t5;
    Canvas control_canvas;
    bool showed=false;
    // Start is called before the first frame update
    void Start()
    {
        button=this.GetComponent<Button>();
        button.onClick.AddListener(showconfirm);
        i1=GameObject.Find("Confirm_Window").GetComponent<Image>();
        i2=GameObject.Find("Confirm_b1").GetComponent<Image>();
        i3=GameObject.Find("Confirm_b2").GetComponent<Image>();
        i4=GameObject.Find("Confirm_b3").GetComponent<Image>();
        i5=GameObject.Find("X").GetComponent<Image>();
        t1=GameObject.Find("confirm_text1").GetComponent<Text>();
        t2=GameObject.Find("confirm_text2").GetComponent<Text>();
        t3=GameObject.Find("confirm_text3").GetComponent<Text>();
        t5=GameObject.Find("XText").GetComponent<Text>();
        b1=GameObject.Find("Confirm_b1").GetComponent<Button>();
        b2=GameObject.Find("Confirm_b2").GetComponent<Button>();
        b3=GameObject.Find("Confirm_b3").GetComponent<Button>();
        b4=GameObject.Find("X").GetComponent<Button>();
        b1.onClick.AddListener(toGameNode);
        b2.onClick.AddListener(toGameNode);
        b3.onClick.AddListener(hiddenwindow);
        b4.onClick.AddListener(hiddenwindow);
        i1.enabled=false;
        i2.enabled=false;
        i3.enabled=false;
        i4.enabled=false;
        i5.enabled=false;
        t1.enabled=false;
        t2.enabled=false;
        t3.enabled=false;
        t5.enabled=false;
        control_canvas=GameObject.Find("ControlCanvas").GetComponent<Canvas>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(control_canvas==null)
        {
            control_canvas=GameObject.Find("ControlCanvas").GetComponent<Canvas>();
        }
        if(showed)
        {
            control_canvas.enabled=false;
        }
        else
        {
            control_canvas.enabled=true;
        }
    }
    void showconfirm()
    {
        if(!showed)
        {
            i1.enabled=true;
            i2.enabled=true;
            i3.enabled=true;
            i4.enabled=true;
            i5.enabled=true;
            t1.enabled=true;
            t2.enabled=true;
            t3.enabled=true;
            t5.enabled=true;
        }
        else
        {
            i1.enabled=false;
           i2.enabled=false;
        i3.enabled=false;
        i4.enabled=false;
        i5.enabled=false;
        t1.enabled=false;
        t2.enabled=false;
        t3.enabled=false;
        t5.enabled=false;
        }
        showed=!showed;
    }
    void toGameNode()
    {
        switchScene("GameNode");
    }

    void hiddenwindow()
    {
        i1.enabled=false;
        i2.enabled=false;
        i3.enabled=false;
        i4.enabled=false;
        i5.enabled=false;
        t1.enabled=false;
        t2.enabled=false;
        t3.enabled=false;
        t5.enabled=false;
        showed=false;
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
