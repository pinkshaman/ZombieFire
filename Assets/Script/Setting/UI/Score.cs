using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Stage Stage;
    public Text rankClass;
    public Text scoreText;
    public Text rankExpText;

    private float rankExp;
    private int score;

    public DamageManagement damageManagement;
    public UnityEvent OnRankChange;
    public void Start()
    {
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);
        OnRankChange.AddListener(UpdateRank);
    }
    public void IntRank()
    {
        
    }
  
    public void GetHeadShotScore()
    {
        score += 100;       
        UpdateRank();
        OnRankChange.Invoke();
    }

    public void GetSkillScore()
    {
        score += 50;
        UpdateRank();
        OnRankChange?.Invoke();
    }
    private void UpdateRank()
    {
        if (rankExp >= 100)
        {
            rankClass.text = "S";
        }
        else if (rankExp >= 70)
        {
            rankClass.text = "A";
        }
        else if (rankExp >= 40)
        {
            rankClass.text = "B";
        }
        else
        {
            rankClass.text = "C";
        }  
        scoreText.text = $"{score}";
        rankExpText.text = $"{rankExp}";
    }

}
