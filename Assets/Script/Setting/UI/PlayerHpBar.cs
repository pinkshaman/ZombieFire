using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : HPBar
{
    public Text healthAmounText;
    public override void Start()
    {
        health = FindFirstObjectByType<PlayerHealth>();
        health.OnHealthChange.AddListener(Fill);
        ShowText();      

    }
    public override void Fill(int currentHealth, int totalHealth)
    {
        var fillPercent = 1f * currentHealth / totalHealth;
        FillBar.fillAmount = fillPercent;
        ShowText();
    }
    public  void ShowText()
    {
        healthAmounText.text = $"HP : {health.HealthPoint}";
    }
}
