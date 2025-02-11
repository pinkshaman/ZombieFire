using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public int level;
    public float exp;
    public int health;
    public int coin;
    public int gold;
    
    public SpecialUpgradeProgess specialUpgradeProgess;
    public ItemList itemList;


    public PlayerData(int level, float exp, int health, int coin, int gold, SpecialUpgradeProgess specialUpgradeProgess)
    {
        this.level = level;
        this.exp = exp;
        this.health = health;
        this.coin = coin;
        this.gold = gold;
        this.specialUpgradeProgess = specialUpgradeProgess;
    }
}
[Serializable]
public class SpecialUpgrade
{
    public string title;
    public int upgradeLevel;
 
   
    public SpecialUpgrade(string title, int level)
    {
        this.title = title;
        this.upgradeLevel = level;
    }
}
[Serializable]
public class SpecialUpgradeProgess
{
    public List<SpecialUpgrade> specialUpdateProgessList;
}

[Serializable]
public class Item
{
    public string itemName;
    public int quatity;

    public Item(string itemName, int quatity)
    {
        this.itemName = itemName;
        this.quatity = quatity;
    }
}
[Serializable]
public class ItemList
{
    public List<Item> itemLists;
}


