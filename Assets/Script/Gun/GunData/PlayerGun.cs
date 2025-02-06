using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGun
{
    public string gunName;
    public bool isUnlocked;
    public int ammoStoraged;
    public int starUpgrade;
    
    public PlayerGun(string gunName,bool isUnlocked, int ammoStoraged, int starUpgrade)
    {
        this.gunName = gunName;
        this.isUnlocked = isUnlocked;
        this.ammoStoraged = ammoStoraged;
        this.starUpgrade = starUpgrade;
    }
}

[Serializable]
public class PlayerGunList
{
    public List<PlayerGun> playerGuns;
}




