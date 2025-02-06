using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public Text coinText;
    public Text goldText;

    private int _coin = 0;
    private int _gold = 0;

    public UnityEvent OnCoinChange;
    public UnityEvent OnGoldChange;
    public int Coin
    {
        get => _coin;
        set
        {
            _coin = Mathf.Max(0, value);
            OnCoinChange.Invoke();
        }
    }
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = Mathf.Max(0, value);
            OnGoldChange.Invoke();
        }
    }
    public void Start()
    {
        OnCoinChange.AddListener(UpdateCurrencyUI);
        OnGoldChange.AddListener(UpdateCurrencyUI);
        UpdateCurrencyUI();
    }
    public void LoadCurrencyData(PlayerData data)
    {
        Coin = data.coin;
        Gold = data.gold;
       
    }
    public void UpdateCurrencyUI()
    {
        coinText.text = Coin.ToString();
        goldText.text = Gold.ToString();
    }
    public void AddCoin(int amount) => Coin += amount;
    public void AddGold(int amount) => Gold += amount;
    public void MinusCoin(int amount) => Coin -= amount;
    public void MinusGold(int amount) => Gold -= amount;
}
