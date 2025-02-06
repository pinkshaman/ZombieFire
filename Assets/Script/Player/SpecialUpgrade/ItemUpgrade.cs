using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUpgrade : SpecialUpgrades
{
    public ItemType type;
    public ItemData data;
    public Text quatity;
    public UnityEvent OnQuatityChange;
    private int _currentQuatity;

    public int CurrentQuatity
    {
        get => _currentQuatity;
        set
        {
            _currentQuatity = Mathf.Max(0, value);
            OnQuatityChange.Invoke();
            quatity.text = _currentQuatity.ToString();
            PlayerManager.Instance.UpdateItemData(data.itemName, CurrentQuatity);
        }
    }
    public void Start()
    {
        data = PlayerManager.Instance.ReturnItemData(type);
        CurrentQuatity = PlayerManager.Instance.ReturnNumberItem(data.itemName);
        quatity.text = CurrentQuatity.ToString();
        
    }
    public int ReturnPrice()
    {
        int price = data.price.price;
        return price;
    }
    public bool IsCanBuy()
    {
        if (data.price.priceType == PriceType.Coin)
        {
            return PlayerManager.Instance.playerData.coin >= data.price.price;
        }
        else
        {
            return PlayerManager.Instance.playerData.gold >= data.price.price;
        }
    }
  
    public void BuyItem()
    {
        if (!IsCanBuy()) return;
        if (data.price.priceType == PriceType.Coin)
        {
            PlayerManager.Instance.playerData.coin -= data.price.price;
        }
        else
        {
            PlayerManager.Instance.playerData.gold -= data.price.price;
        }
        CurrentQuatity += data.quatityPerTimes;
        quatity.text = CurrentQuatity.ToString();
    }
    
    public void UseItem()
    {
        if (CurrentQuatity > 0)
        {
            CurrentQuatity--;    
        }
    }
    public void AddItem(int amount)
    {
        CurrentQuatity += amount;
    }
}
