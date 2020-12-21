using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class zanting : MonoBehaviour
{
    Button button;
    Text button_text;
    // Start is called before the first frame update
    void Start()
    {
        button=this.GetComponent<Button>();
        button.onClick.AddListener(pease);
        button_text=GameObject.Find("zantingText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pease()
    {
        if(Time.timeScale!=0)
        {
            Time.timeScale = 0;
            button_text.text="继续";
        }
        else
        {
            Time.timeScale = 1;
            button_text.text="暂停";
        }
        
    }
}
