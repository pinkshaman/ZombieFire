using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string playerID;
    public float exp;
    public int health;
    public int coin;
    public int gold;

    public SpecialUpgradeProgess specialUpgradeProgess;
    public ItemList itemList;
    public LevelRewardProgessList levelRewardProgessList;

    public PlayerData(LevelRewardProgessList levelRewardProgessList, float exp, int health, int coin, int gold, SpecialUpgradeProgess specialUpgradeProgess, string playerID)
    {
        this.levelRewardProgessList = levelRewardProgessList;
        this.exp = exp;
        this.health = health;
        this.coin = coin;
        this.gold = gold;
        this.specialUpgradeProgess = specialUpgradeProgess;
        this.playerID = playerID; 
    }
}
[Serializable]
public class LeveRewardProgess
{
    public int levelProgess;
    public bool isTook;
    public LeveRewardProgess(int levelProgess, bool isTook)
    {
        this.levelProgess = levelProgess;
        this.isTook = isTook;
    }   
}
[Serializable]
public class LevelRewardProgessList
{
    public List<LeveRewardProgess> leveRewardProgesses;
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


