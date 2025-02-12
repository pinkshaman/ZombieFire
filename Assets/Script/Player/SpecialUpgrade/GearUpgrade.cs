using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgrade : SpecialUpgrades
{
    public GearUpgradeType type;
    public GearUpgradeData data;
    public Text upgradeLevel;

    private int currentLevel;

    public void Start()
    {
        data = PlayerManager.Instance.ReturnSpecialUpgradeData(type);
        currentLevel = PlayerManager.Instance.ReturnLevelUpgrade(data.Title);
        upgradeLevel.text = currentLevel.ToString();
    }
    public int ReturnInfor()
    {
        int power = data.system.upgradeSystem[currentLevel].percentIncrease;
        return power;
    }
    public int ReturnPrice()
    {
        if (currentLevel+1 >= data.system.upgradeSystem.Count) return 0;

        int upgradeCost = data.system.upgradeSystem[currentLevel + 1].priceUpgrade;
        return upgradeCost;
    }
    public bool CanUpgrade()
    {
        if (currentLevel+1 >= data.system.upgradeSystem.Count) return false;

        int upgradeCost = data.system.upgradeSystem[currentLevel+1].priceUpgrade;
        return PlayerManager.Instance.playerData.coin >= upgradeCost;
    }
    public void Upgrade()
    {
        if (!CanUpgrade()) return;
        int upgradeCost = data.system.upgradeSystem[currentLevel+1].priceUpgrade;
        PlayerManager.Instance.playerData.coin -= upgradeCost;
        currentLevel++;
        upgradeLevel.text = currentLevel.ToString();
        PlayerManager.Instance.UpgradeGearEffect(data.Title, currentLevel);
    }
    
   
}
