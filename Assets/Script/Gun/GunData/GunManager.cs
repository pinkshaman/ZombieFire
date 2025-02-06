using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Guntype
{
    Pistol,
    Rifle,
    Shotgun,
    Sniper,
    SMG,
    Launcher
}
[Serializable]
public class GunStats
{
    public int ammoCapacity;
    public float fireRate;
    public int damage;
    public float critical;
}
[Serializable]
public class GunModel
{
    public Guntype gunType;
    public GameObject gunModel;
    public Sprite gunSprite;
}
[Serializable]
public class GunAudio
{
    public AudioClip Ready;
    public AudioClip Shooting;
    public AudioClip Reload;
    public AudioClip Hide;
    public AudioClip DryFire;

}

[Serializable]
public class BaseGun
{
    [Header("General Properties")]
    public string GunName;
    public GunStats gunStats;
    public GunModel gunModel;
    public GunUpgradeList upgradeList;
}
[Serializable]
public class GunUpgrade
{
    public float fireRateUpgrade;
    public int powerUpgrade;
    public float criticalUpgrade;
    public float reloadUpgrade;
}
[Serializable]
public class GunUpgradeList
{
    public List<GunUpgrade> gunUgradeList;
}

public class GunManager : MonoBehaviour
{
    public static GunManager Instance { get; private set; }

    public GunList gunList;
    public PlayerGunList playerGunList;
    public GunSwicher gunSwicher;

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
        LoadGunData();
       
    }

    public PlayerGun GetPlayerGun(string gunName)
    {       
        var gunData = playerGunList.playerGuns.Find(gunData => gunData.gunName == gunName);
        return gunData;
    }
    [ContextMenu("CreatPlayerGunData")]
    public void CreatPlayerGunData()
    {
        playerGunList = new PlayerGunList();
        playerGunList.playerGuns = new List<PlayerGun>();
        foreach (var gun in gunList.baseGunList)
        {
            playerGunList.playerGuns.Add(new PlayerGun(gun.GunName, false, 0, 0));
        }
        SaveGunData();
    }
    public Gun FindActiveGun()
    {
        Gun[] guns = FindObjectsOfType<Gun>();
        foreach (Gun gun in guns)
        {
            if (gun.gameObject.activeInHierarchy)
            {
                return gun;
            }
        }
        return null;
    }
    public BaseGun GetGun(string gunName)
    {
        var gunData = gunList.baseGunList.Find(gun => gun.GunName == gunName);
        return gunData;
    }
    [ContextMenu("SaveGunData")]
    public void SaveGunData()
    {
        var value = JsonUtility.ToJson(playerGunList);
        PlayerPrefs.SetString(nameof(playerGunList), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadGunData")]
    public void LoadGunData()
    {
        var defaultValue = JsonUtility.ToJson(playerGunList);
        var json = PlayerPrefs.GetString(nameof(playerGunList), defaultValue);
        playerGunList = JsonUtility.FromJson<PlayerGunList>(json);
        Debug.Log("PlayerGun is Loaded");
    }


}


