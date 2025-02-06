using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GearUpgradeData 
{
    public GearUpgradeType type;
    public string Title;
    public string Decription;
    public Sprite priceImage;
    public UpgradeSystem system;    
}
[Serializable]
public class UpgradeBase
{
    public int levelUpgrade;
    public int priceUpgrade;
    public int percentIncrease;
    
}
[Serializable]
public class UpgradeSystem
{
    public List<UpgradeBase> upgradeSystem;
}

public enum GearUpgradeType
{
    Hat,
    Mask,
    Jacket,
    Trouser,
    Bag,
}

