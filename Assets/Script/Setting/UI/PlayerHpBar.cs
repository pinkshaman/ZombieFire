using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : HPBar
{

    public override void Start()
    {
        health = FindObjectOfType<PlayerHealth>();
        health.OnHealthChange.AddListener(Fill);
        ShowText();      

    }
    public override void Fill(int currentHealth, int totalHealth)
    {
        var fillPercent = 1f * currentHealth / totalHealth;
        FillBar.fillAmount = fillPercent;
        ShowText();
    }
    public override void ShowText()
    {
        healthAmounText.text = $"HP : {health.HealthPoint}";
    }
}
