using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BonusScore : MonoBehaviour
{
    public int kill;
    public int coin;
    public Text textCoin;
    public DamageManagement damageManagement;

    public void Start()
    {
        damageManagement = FindFirstObjectByType<DamageManagement>();
        coin = 0;
        kill = 0;
        damageManagement.OnHeadShot.AddListener(GetHeadShotScore);
        damageManagement.OnKill.AddListener(GetSkillScore);
        UpdateUI();
    }

    public void GetHeadShotScore()
    {
        kill++;
        coin += 100;
        UpdateUI();
    }
    public void GetSkillScore()
    {
        kill++;
        coin += 200;
        UpdateUI();
    }
    private void UpdateUI()
    {
        textCoin.text = $"{coin}";
    }
    public int ReturnCoin()
    {
        return coin;
    }
    public int ReturnKill()
    {
        return kill;
    }
}
