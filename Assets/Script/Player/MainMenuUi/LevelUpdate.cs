using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpdate : MonoBehaviour
{
    public Text levelText;    
    public Text expPercentText; 

    private int playerLevel = 1;    
    private float playerExp = 0f;     
    private float nextLevelExp = 100f; 
    private float expIncreaseRate = 1.3f; 

    public void SetExp(PlayerData data)
    {
        playerLevel = 1;
        nextLevelExp = 100f;
        playerExp = data.exp;
        UpdateLevel();
    }
    public void AddExp(float amount)
    {
        playerExp += amount;
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        while (playerExp >= nextLevelExp)
        {
            playerExp -= nextLevelExp;
            playerLevel++;
            nextLevelExp *= expIncreaseRate; 
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        float expPercent = (playerExp / nextLevelExp) * 100f;
        levelText.text = $"{playerLevel}";      
        expPercentText.text = $"{expPercent:F2}%";
    }
}
