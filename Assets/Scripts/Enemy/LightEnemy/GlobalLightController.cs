using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using GameLogic.EntityStats;
using GameLogic.Managers;

public class GlobalLightController : MonoBehaviour
{
    public Light2D light2d;
    public Stats stat;
    private bool isEnd = false;

    private float currentLight = 1;
    private float vel;

    private void Start()
    {
        stat.GetComponent<EntityManager>().OnObjectVisuallyDeath += End;
    }

    private void Update()
    {
        if (!isEnd && stat.health < 0.5 * stat.maxHealth)
        {
            currentLight = Mathf.SmoothDamp(currentLight, 0.3f, ref vel, 0.8f);
            ChangeGlobalLight(currentLight);
        }
        
        if(GameManager.instance.gameHasEnd)
        {
            ChangeGlobalLight(1);
        }
    }

    private void End(EntityManager entity)
    {
        isEnd = true;
    }

    public void ChangeGlobalLight(float color)
    {
        light2d.color = new Vector4(color, color, color,1);
    }
}
