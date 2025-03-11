using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : HPBar
{
    public GameObject zombieCount;

    public override void Start()
    {
        zombieCount.SetActive(false);
        
    }
    public void ActiveZombieCount()
    {
        zombieCount.SetActive(true);
    }
    public override void Fill(int currentHealth, int totalHealth)
    {
        var fillPercent = 1f * currentHealth / totalHealth;
        FillBar.fillAmount = fillPercent;
    }
}
