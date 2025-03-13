using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public int goldQuatity;
    public int realMoney;

    public int percentBonus;
    public bool isBonus;

    public Text priceText;
    public Text goldQuatityText;
    public Text bonusText;
    public GameObject bonusLabel;
    public PurchasedPanel purchasedPanel;
    public bool isBought;

    public void Start()
    {
        priceText.text = realMoney.ToString();
        goldQuatityText.text = goldQuatity.ToString();
        bonusLabel.SetActive(isBonus);
        if (!isBonus) return;
        bonusText.text = $"BONUS {percentBonus} %";
    }
    public void Buy()
    {
        Debug.Log(" Buy Gold");
        isBought = true;
        var playerData = PlayerManager.Instance.playerData;
        if(isBonus)
        {
            playerData.gold += goldQuatity;
            PlayerManager.Instance.UpdatePlayerData(playerData);
            purchasedPanel.gameObject.SetActive(true);
            purchasedPanel.SetData(goldQuatity);
        }
        else
        {
            var newGold = Mathf.RoundToInt(goldQuatity * (1 + percentBonus / 100));
            playerData.gold += newGold;
            PlayerManager.Instance.UpdatePlayerData(playerData);
            purchasedPanel.gameObject.SetActive(true);
            purchasedPanel.SetData(newGold);
        }
    }
    
}
