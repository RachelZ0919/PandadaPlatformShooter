using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameLogic.EntityStats;


public class HP_slider : MonoBehaviour
{
    public Slider health_slider;
    Text health_text;
    float value,max;
    public  Stats stat;
    // Start is called before the first frame update
    void Start()
    {
        value=stat.health;
        max=stat.maxHealth;
        health_slider = GameObject.Find("HP_Slider").GetComponent<Slider>();
        health_text = GameObject.Find("HP_text_actually").GetComponent<Text>();
        value=stat.maxHealth;
        max=stat.maxHealth;
        health_slider.value=0;
    }

    // Update is called once per frame
    void Update()
    {
        value=stat.health;
        max=stat.maxHealth;
        float percent_value=value/max;
        health_slider.value=1.0f-percent_value;
        health_text.text="HP: "+value+" / "+max;
    }
}
