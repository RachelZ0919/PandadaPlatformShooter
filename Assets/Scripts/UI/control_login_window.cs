using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_login_window : MonoBehaviour
{
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count=0;
        this.GetComponent<CanvasGroup>().alpha=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count<100)
        {
            count++;
        }
        else
        {
            this.GetComponent<CanvasGroup>().alpha=1;
        }
        
    }
}
