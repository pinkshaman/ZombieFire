using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ItemData 
{
    public ItemType itemType;
    public string itemName;
    public string decription;
    public int quatityPerTimes;
    public Price price;
    public int effect;
}

public enum ItemType
{
    QuickReload,
    Grenade,
    Shield,
    BonusExP,
}
public enum PriceType
{
    Gold,
    Coin,
}
[Serializable]
public class Price
{
    public PriceType priceType;
    public int cost;
    public Sprite priceImage;
}
