using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
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
    public PurchasedPanel purchasedPanel;
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

        if (playerdata.gold >= goldPrirce)
        {
            playerdata.gold -= goldPrirce;

            if (isBonus)
            {
                playerdata.coin += coinQuatity;
                PlayerManager.Instance.UpdatePlayerData(playerdata);
                purchasedPanel.gameObject.SetActive(true);
                purchasedPanel.SetData(coinQuatity);
            }
            else
            {
                var newQuatity = Mathf.RoundToInt(coinQuatity * (1 + percentBonus / 100f));
                playerdata.coin += newQuatity;
                PlayerManager.Instance.UpdatePlayerData(playerdata);
                purchasedPanel.gameObject.SetActive(true);
                purchasedPanel.SetData(newQuatity);
            }
        }
    }
}

