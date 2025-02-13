using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public PlayerData playerData;
    public GearUpgradeList gearUpgradeList;
    public ItemDataList itemDataList;

    public UnityEvent<PlayerData> OnPlayerDataChange;

    private PlayerData playerDataAftereffected;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void Start()
    {
        LoadPlayerData();

        if (playerData.specialUpgradeProgess == null || playerData.specialUpgradeProgess.specialUpdateProgessList == null)
        {
            InitNewGearUpgrade();
        }
    }
    [ContextMenu("InitNewGearUpgrade")]
    public void InitNewGearUpgrade()
    {
        playerData.specialUpgradeProgess = new SpecialUpgradeProgess();
        playerData.specialUpgradeProgess.specialUpdateProgessList = new List<SpecialUpgrade>();

        foreach (var upgrade in gearUpgradeList.gearUpgradeLists)
        {
            playerData.specialUpgradeProgess.specialUpdateProgessList.Add(new SpecialUpgrade(upgrade.Title.ToString(), 0));
        }

        SavePlayerData();
        UpdatePlayerData(playerData);
    }
    [ContextMenu("InitNewItem")]
    public void InitNewItem()
    {
        playerData.itemList = new ItemList();
        playerData.itemList.itemLists = new List<Item>();
        foreach (var item in itemDataList.itemDataLists)
        {
            playerData.itemList.itemLists.Add(new Item(item.itemName, 0));
        }
    }
    public GearUpgradeData ReturnSpecialUpgradeData(GearUpgradeType type)
    {
        var data = gearUpgradeList.gearUpgradeLists.Find(data => data.type == type);
        return data;
    }
    public ItemData ReturnItemData(ItemType type)
    {
        var data = itemDataList.itemDataLists.Find(data => data.itemType == type);
        return data;
    }
    public int ReturnNumberItem(string name)
    {
        var data = playerData.itemList.itemLists.Find(data => data.itemName == name);
        return data.quatity;
    }
    public int ReturnLevelUpgrade(string title)
    {
        var data = playerData.specialUpgradeProgess.specialUpdateProgessList.Find(data => data.title == title);
        return data.upgradeLevel;
    }
    public int ReturnPriceUpgrade(int level, GearUpgradeType type)
    {
        var data = gearUpgradeList.gearUpgradeLists.Find(data => data.type == type);
        var dataUpgrade = data.system.upgradeSystem.Find(data => data.levelUpgrade == level);
        return dataUpgrade.priceUpgrade;
    }

    public bool ReturnItemReloadInfor()
    {
        foreach (var item in playerData.itemList.itemLists)
        {
            if (item.itemName == "QuickReload")
            {
                if (item.quatity > 0)
                {
                    
                    return true;
                }
            }
        }
        return false;
    }
    public void UpgradeGearEffect(string title, int level)
    {
        var data = playerData.specialUpgradeProgess.specialUpdateProgessList.Find(data => data.title == title);
        data.upgradeLevel = level;
        UpdatePlayerData(playerData);
    }
    public void UpdateItemData(string name, int quatity)
    {
        var data = playerData.itemList.itemLists.Find(data => data.itemName == name);
        data.quatity = quatity;
        UpdatePlayerData(playerData);
    }
    public void UpdatePlayerData(PlayerData changedData)
    {
        this.playerData = changedData;
        OnPlayerDataChange.Invoke(playerData);
        SavePlayerData();
    }
    public int ReturnGearEffect(GearUpgradeType type)
    {
        var gear = gearUpgradeList.gearUpgradeLists.Find(gear => gear.type == type);
        var level = playerData.specialUpgradeProgess.specialUpdateProgessList.Find(level => level.title == gear.Title);
        var upgrade = gear.system.upgradeSystem.Find(upgrade => upgrade.levelUpgrade == level.upgradeLevel);
        return upgrade.percentIncrease;

    }
   
    public PlayerData ReturnPlayerDataAfterEffected()
    {
        foreach (var upgradeData in playerData.specialUpgradeProgess.specialUpdateProgessList)
        {
            var gearUpgrade = gearUpgradeList.gearUpgradeLists.Find(upgrade => upgrade.Title == upgradeData.title);
            var level = gearUpgrade.system.upgradeSystem.Find(level => level.levelUpgrade == upgradeData.upgradeLevel);
            if (gearUpgrade.type == GearUpgradeType.Jacket)
            {
                if (level.levelUpgrade > 0)
                {
                   var newHealth = Mathf.RoundToInt(playerData.health * (1 + level.percentIncrease / 100f));
                    playerData.health = newHealth;
                    Debug.Log($"PlayerHealth: {playerData.health}");
                    return playerData;
                }
            }
        }
        return playerData;
    }
 
    [ContextMenu("SavePlayerData")]
    public void SavePlayerData()
    {
        var value = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(nameof(playerData), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadPlayerData")]
    public void LoadPlayerData()
    {
        var defaultValue = JsonUtility.ToJson(playerData);
        var json = PlayerPrefs.GetString(nameof(playerData), defaultValue);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("PlayerData is Loaded");
    }

}
