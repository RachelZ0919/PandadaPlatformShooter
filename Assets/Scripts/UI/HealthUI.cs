using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameLogic.EntityStats;


public class HealthUI : MonoBehaviour
{
    private RectTransform hpBackground;
    private RectTransform hpBar;

    private float backgroundRight;
    private float barRight;

    [SerializeField] private float initialHP;

    [SerializeField] private Stats playerStat;
    [SerializeField] private float pixelSize = 3.125f;

    private void Awake()
    {
        hpBackground = transform.Find("HPBackground").GetComponent<RectTransform>();
        hpBar = transform.Find("HPBar").GetComponent<RectTransform>();

        backgroundRight = hpBackground.offsetMax.x;
        barRight = hpBar.offsetMax.x;

        playerStat.OnStatsChanged += OnStatChanged;

        UpdateUI(initialHP, initialHP);
    }

    private void OnStatChanged(Stats stat)
    {
        UpdateUI(stat.maxHealth, stat.health);
    }

    private void UpdateUI(float maxHealth,float health)
    {
        hpBackground.offsetMax = new Vector2(backgroundRight + (maxHealth - 1) * pixelSize * 10, hpBackground.offsetMax.y);
        hpBar.offsetMax = new Vector2(barRight + (health - 1) * pixelSize * 10, hpBar.offsetMax.y);
    }
}
