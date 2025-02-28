using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin :MonoBehaviour
{
    public int coinQuatity;
    public int goldPrirce;
    public Button buttonBuy;
    public int percentBonus;
    public bool isBonus;

    public Text coinQuatityText;
    public Text goldQuatityText;
    public Text bonusText;
    public GameObject bonusLael;
    
    public void Start()
    {
        coinQuatityText.text = coinQuatity.ToString();
        goldQuatityText.text = goldPrirce.ToString();
        buttonBuy.onClick.AddListener(Buy);
        if (!isBonus) return;
        bonusLael.SetActive(isBonus);
        bonusText.text = $"BONUS {percentBonus}%";
    }
    public void Buy()
    {
        var playerdata = PlayerManager.Instance.playerData;
        playerdata.gold -= goldPrirce;

        if (isBonus)
        {
            playerdata.coin += coinQuatity;
            PlayerManager.Instance.UpdatePlayerData(playerdata);
        }
        else
        {
            var newQuatity = Mathf.RoundToInt(coinQuatity * (1 + percentBonus / 100f));
            playerdata.coin += newQuatity;
            PlayerManager.Instance.UpdatePlayerData(playerdata);
        }
    }
}

