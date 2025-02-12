using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameFlow gameFlow;
    public Text rankClass;
    public Text scoreText;
    public Text rankExpText;

    private float rankExp;
    private int score;
    private float expPerZombie;
    private float expPerRank;

    private List<string> rankOrder = new List<string> { "C", "B", "A", "S" }; 

    public DamageManagement damageManagement;

    private void Awake()
    {
        // Đảm bảo GameObject UI đã khởi tạo trước khi truy cập
        if (rankClass == null || scoreText == null || rankExpText == null)
        {
            Debug.LogError("UI Text References are missing!");
            return;
        }
    }
    private void Start()
    {
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);

        expPerZombie = IntRank();
        expPerRank = 25f;
        rankClass.text = rankOrder[0];
        UpdateUI();
    }

    public float IntRank()
    {
        int totalZombie = 0;
        Debug.Log($"Stage: {gameFlow.stage.stageID}");

        foreach (var BaseWave in gameFlow.stage.waveList)
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
        CheckRankUp();
    }

    public void GetSkillScore()
    {
        score += 20;
        rankExp += (expPerZombie * 0.5f);
        CheckRankUp();
    }

    private void CheckRankUp()
    {
        if (rankExp >= expPerRank)
        {
            rankExp -= expPerRank;
            IncreaseRankClass();
        }

        UpdateUI();
    }

    private void IncreaseRankClass()
    {
        int currentRankIndex = rankOrder.IndexOf(rankClass.text);

        if (currentRankIndex < rankOrder.Count - 1)
        {
            rankClass.text = rankOrder[currentRankIndex + 1];
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

}
