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
    public DamageManagement damageManagement;

    public void Start()
    {
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);
        expPerZombie = IntRank();
        expPerRank = expPerZombie * 25;
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
        float exp = 100 / (float)totalZombie;

        return exp;
    }

    public void GetHeadShotScore()
    {
        score += 40;
        rankExp += expPerZombie;
        UpdateRank(score, rankExp);
    }

    public void GetSkillScore()
    {
        score += 20;
        rankExp += (expPerZombie / 100 * 80);
        UpdateRank(score, rankExp);
    }
    private void UpdateRank(int score, float exp)
    {
        float x = exp;
        Debug.Log($"Score {score}, Exp:{exp}");
        if (exp >= (expPerRank * 4))
        {
            rankClass.text = "S";
        }
        else if (exp >= (expPerRank * 3))
        {
            rankClass.text = "A";
        }
        else if (exp >= (expPerRank * 2))
        {
            rankClass.text = "B";
            
        }
        else
        {
            rankClass.text = "C";
        }
        scoreText.text = $"{score}";
        rankExpText.text = $"{exp}%";
    }

}
