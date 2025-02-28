using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public int goldQuatity;
    public int realMoney;
    public Button buttonBuy;
    public int percentBonus;
    public bool isBonus;

    public Text priceText;
    public Text goldQuatityText;
    public Text bonusText;
    public GameObject bonusLabel;

    public void Start()
    {
        priceText.text = realMoney.ToString();
        goldQuatityText.text = goldQuatity.ToString();
        buttonBuy.onClick.AddListener(Buy);
        bonusLabel.SetActive(isBonus);
        if (!isBonus) return;
        bonusText.text = $"BONUS {percentBonus} %";
    }
    public void Buy()
    {
        Debug.Log(" Buy Gold");
    }
}
