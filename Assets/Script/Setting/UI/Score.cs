using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    public Text rankClass;
    public Text scoreText;
    public Text rankExpText;

    private int killCount;
    private int headshotCount;
    private float rankExp;
    private int score;
    private float expPerZombie;
    private float expPerRank;
    private Stage stage;
    private List<string> RankOrder;

    private float totalExp;

    public DamageManagement damageManagement;

    public void Start()
    {
        damageManagement = FindObjectOfType<DamageManagement>();
        stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
        RankOrder = new List<string> { "C", "B", "A", "S" };
        expPerZombie = IntRank();
        expPerRank = 25f;
        rankClass.text = RankOrder[0];
        killCount = 0;
        headshotCount = 0;
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);
        UpdateUI();
    }

    public float IntRank()
    {
        int totalZombie = 0;
        foreach (var BaseWave in stage.waveList)
        {
            foreach (var group in BaseWave.zombieList)
            {
                totalZombie += group.quatity;
            }
        }
        float exp = 100f / totalZombie;
        return exp;
    }

    public void GetHeadShotScore()
    {
        score += 40;
        rankExp += expPerZombie;
        totalExp += expPerZombie;
        headshotCount++;
        CheckRankUp();
        UpdateUI();
    }

    public void GetSkillScore()
    {
        score += 20;
        rankExp += (expPerZombie * 0.6f);
        totalExp += (expPerZombie * 0.6f);
        killCount++;    
        CheckRankUp();
        UpdateUI();
    }

    private void CheckRankUp()
    {
        if (rankExp < expPerRank) return;
        if (rankExp >= expPerRank)
        {
            rankExp -= expPerRank;
            IncreaseRankClass();
        }
        UpdateUI();
    }

    private void IncreaseRankClass()
    {
        int currentRankIndex = RankOrder.IndexOf(rankClass.text);
        if (currentRankIndex < RankOrder.Count - 1)
        {
            rankClass.text = RankOrder[currentRankIndex + 1];
        }
        else
        {
            Debug.Log(" rank is max");
        }
    }

    private void UpdateUI()
    {
        scoreText.text = $"{score}";
        float rankExpPercentage = (rankExp / expPerRank) * 100f;
        rankExpText.text = $"{rankExpPercentage:F2}%";
    }
    public string ReturnRank()
    {
        return rankClass.text;
    }
    public string ReturnScore()
    {
        return score.ToString();
    }
    public string ReturnExp()
    {
        return totalExp.ToString();
    }
    public int ReturnRewardCoin()
    {
        return stage.coinReward;
    }
    public string ReturnKillCount()
    {
        return killCount.ToString();
    }
    public string ReturnHeadShotCount()
    {
        return headshotCount.ToString();
    }
}
