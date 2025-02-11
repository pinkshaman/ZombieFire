
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
    public string gunDecription;
    public AnimationClip reloadAnimationClip;
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
    public BuyGun buyGun;
}
[Serializable]
public class GunUpgrade
{
    public int starUpgrade;
    public int priceUpgrade;
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
[Serializable]
public class GunSlot
{
    public string gunSlot1;
    public string gunSlot2;
}
[Serializable]
public class BuyGun
{
    public int ammoPrice;
    public Price price;
}

public class GunManager : MonoBehaviour
{
    public static GunManager Instance { get; private set; }

    public GunList gunList;
    public PlayerGunList playerGunList;
    public GunSwicher gunSwicher;
    public GunSlot gunSlot;
    public GunInventory gunInventory;
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
        DontDestroyOnLoad(gameObject);
    }
    public void CreateWeaponInventory()
    {
        foreach (var gun in gunList.baseGunList)
        {
            var progessData = playerGunList.playerGuns.Find(progessData => progessData.gunName == gun.GunName);
            if (gun.GunName == "RPG")
                continue;
            if (progessData == null)
            {
                CreatPlayerGunData(gun);
            }
            gunInventory.CreateGunUI(gun, progessData, gunSlot);
        }
    }
    public void UpdateGunSlot(string gunSlot1Name, string gunSlot2Name)
    {
        gunSlot.gunSlot1 = gunSlot1Name;
        gunSlot.gunSlot2 = gunSlot2Name;
        SaveGunSlot();
    }
    public float ReturnReloadTimes(string gunName)
    {
        var gunData = gunList.baseGunList.Find(gunData => gunData.GunName == gunName);
        float reloadTime = gunData.gunModel.reloadAnimationClip.length;
        return reloadTime;

    }

    public void UpdateAmmo(string gunName, int ammo)
    {
        var gunData = playerGunList.playerGuns.Find(gunData => gunData.gunName == gunName);
        gunData.ammoStoraged = ammo;
        //SaveGunData();
    }
    public void UpdateGunData(string gunName, PlayerGun dataChanged)
    {
        var gunData = playerGunList.playerGuns.Find(gunData => gunData.gunName == gunName);
        gunData = dataChanged;
        //SaveGunData();
    }
    public PlayerGun GetPlayerGun(string gunName)
    {
        var gunData = playerGunList.playerGuns.Find(gunData => gunData.gunName == gunName);
        return gunData;
    }

    public void CreatPlayerGunData(BaseGun gun)
    {
        playerGunList.playerGuns.Add(new PlayerGun(gun.GunName, false, 1, 1));
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

    [ContextMenu("SaveGunSlot")]
    public void SaveGunSlot()
    {
        var value = JsonUtility.ToJson(gunSlot);
        PlayerPrefs.SetString(nameof(gunSlot), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadGunSlot")]
    public void LoadGunSlot()
    {
        var defaultValue = JsonUtility.ToJson(gunSlot);
        var json = PlayerPrefs.GetString(nameof(gunSlot), defaultValue);
        gunSlot = JsonUtility.FromJson<GunSlot>(json);
        Debug.Log("GunSlot is Loaded");
    }
}


