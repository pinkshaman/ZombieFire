using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalScore : MonoBehaviour
{
    private int killCount;
    private int headshotCount;
    private int score;
    public DamageManagement damageManagement;

    public Text scoreText;
    public void Start()
    {
        damageManagement = FindFirstObjectByType<DamageManagement>();
        killCount = 0;
        headshotCount = 0;
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);
        UpdateUI();
    }

    public void GetHeadShotScore()
    {
        score += 40;
        headshotCount++;
        UpdateUI();
    }
    public void GetSkillScore()
    {
        score += 20;
        killCount++;
        UpdateUI();
    }
    private void UpdateUI()
    {
        scoreText.text = $"{score}";
    }
    public int ReturnKillCount()
    {
        return killCount;
    }
    public int ReturnHeadShotCount()
    {
        return headshotCount;
    }
    public int ReturnScoreCount()
    {
        return score;
    }
    public int ReturnCoin()
    {
        var coin = score *= killCount;
        var newcoin = Mathf.RoundToInt(coin * (1 + headshotCount / 100));
        return newcoin;
    }
}
