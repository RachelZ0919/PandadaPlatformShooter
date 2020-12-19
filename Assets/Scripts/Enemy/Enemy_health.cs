using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameLogic.EntityStats;

public class Enemy_health : MonoBehaviour
{
    public GameObject target_obj;
    public Transform target;
    public Vector3 myoffset;
    float value,max;
    public  Stats stat;
    Slider health_slider;
    public float xlength,ylength;
 
    // Use this for initialization
    void Start()
    {
        health_slider = GameObject.Find("Slider").GetComponent<Slider>();
        xlength=target_obj.GetComponent<SpriteRenderer>().bounds.size.x*30;
        ylength=target_obj.GetComponent<SpriteRenderer>().bounds.size.y*7;
        myoffset= new Vector3(0, 3, 0);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(xlength,ylength);
        value=stat.maxHealth;
        max=stat.maxHealth;
        health_slider.value=0;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(stat.health==0)
        {
            this.gameObject.SetActive(false);
        }
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position+myoffset);
            value=stat.health;
            max=stat.maxHealth;
            float percent_value=value/max;
            health_slider.value=1.0f-percent_value;
        }
        

    }
}
