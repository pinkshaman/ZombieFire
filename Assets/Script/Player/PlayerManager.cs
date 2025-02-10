using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerUI player;
    public PlayerData playerData;
    public GearUpgradeList gearUpgradeList;
    public ItemDataList itemDataList;

    public UnityEvent<PlayerData> OnPlayerDataChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        LoadPlayerData();
        player.SetDataPlayer(playerData);
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
            if(item.itemName == "QuickReload")
            {
                if(item.quatity>0)
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
