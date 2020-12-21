using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class show_menu : MonoBehaviour
{
    Button button;
    Image i1,i2,i3,i4;
    Text t1,t2;
    bool showed=false;
    // Start is called before the first frame update
    void Start()
    {
        button=this.GetComponent<Button>();
        button.onClick.AddListener(showmenu);
        i1=GameObject.Find("menu").GetComponent<Image>();
        i2= GameObject.Find("zanting").GetComponent<Image>();
        i3=GameObject.Find("tuichu").GetComponent<Image>();
        i4=GameObject.Find("line").GetComponent<Image>();
        t1=GameObject.Find("zantingText").GetComponent<Text>();
        t2=GameObject.Find("tuicuText").GetComponent<Text>();
        i1.enabled=false;
        t1.enabled=false;
        t2.enabled=false;
        i2.enabled=false;
        i3.enabled=false;
        i4.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void showmenu()
    {
        if(!showed)
        {
            i1.enabled=true;
            t1.enabled=true;
            t2.enabled=true;
            i2.enabled=true;
            i3.enabled=true;
            i4.enabled=true;
        }
        else
        {
             i1.enabled=false;
        t1.enabled=false;
        t2.enabled=false;
        i2.enabled=false;
        i3.enabled=false;
        i4.enabled=false;
        }
        showed=!showed;
        

    }
    
}
