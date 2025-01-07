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
public class BaseGun 
{    
    [Header("General Properties")]
    public string GunName;
    public GameObject BaseModel;
    public Guntype GunType;
    public int AmmoCapacity;
    public float FireRate;
    public float ReloadTime;
    public int Damage;
    [Header("Animations")]
    public AnimationClip Ready;
    public AnimationClip Fire;
    public AnimationClip Reload;
    public AnimationClip Hide;

    [Header("Audio Clips")]
    public AudioClip ShootingSound;
    public AudioClip ReloadSound;
    public AudioClip ReadySound;
    public AudioClip OutofAmmoSound;

    public void InitWeapon(string gunName, GameObject baseModel, Guntype gunType, int ammoCapacity, float fireRate, float reloadTime, int damage, AnimationClip ready, AnimationClip fire, AnimationClip reload, AnimationClip hide, AudioClip shootingSound, AudioClip reloadSound, AudioClip readySound, AudioClip outofAmmoSound)
    {
        GunName = gunName;
        BaseModel = baseModel;
        GunType = gunType;
        AmmoCapacity = ammoCapacity;
        FireRate = fireRate;
        ReloadTime = reloadTime;
        Damage = damage;
        Ready = ready;
        Fire = fire;
        Reload = reload;
        Hide = hide;
        ShootingSound = shootingSound;
        ReloadSound = reloadSound;
        ReadySound = readySound;
        OutofAmmoSound = outofAmmoSound;
    }
}

public class GunManager : MonoBehaviour
{
    public static GunManager Instance { get; private set; }
    public GunList gunList;
    public PlayerGunList playerGunList;


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


