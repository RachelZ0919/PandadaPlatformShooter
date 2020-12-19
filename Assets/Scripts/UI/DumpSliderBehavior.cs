using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DumpSliderBehavior : MonoBehaviour
{
    Slider slider;
    private float intervalValue = 0.01f;

    private void Awake()
    {
        slider = GetComponent<Slider>();        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        slider.value += intervalValue;
        if (slider.value == slider.maxValue)
        {
            Debug.LogWarning(slider.value);
            slider.value = 0;
        }
    }
}
