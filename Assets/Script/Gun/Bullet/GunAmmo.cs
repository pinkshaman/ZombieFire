using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour
{
    public string gunName;
    public Gun gun;
    public int magSize;
    private int _loadedAmmo;
    public bool isReloadComplete;
    public UnityEvent loadedAmmoChanged;
    
    public int LoadedAmmo
    {
        get => _loadedAmmo;
        set
        {
            _loadedAmmo = value;
            loadedAmmoChanged.Invoke();
            if (_loadedAmmo <= 0)
            {
                LockShooting();
            }
            else
            {
                UnlockShooting();
            }
        }
    }

    private void Start()
    {
        InitializeGun();
        gun.OnShooting.AddListener(SingleFireAmmoCounter);
        gun.OnSwitching.AddListener(OnSelectedGun);
    }
    public void SingleFireAmmoCounter()
    {
        LoadedAmmo--;       
    }
    private void LockShooting()
    {
        gun.enabled = false;
    }
    private void UnlockShooting()
    {
        gun.enabled = true;
    }
    private void ReFillAmmo()
    {
        LoadedAmmo = magSize;
    }
   
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (LoadedAmmo >= magSize)
            {
                return;
            }
            else
            {
                Reload();
            }
        }
    }
    public void Reload()
    {      
        LockShooting();
        gun.ReLoading();
                  
    }
    public void AddAmmo()
    {
        ReFillAmmo();
    }
    public void OnCancelReload()
    {
        if (Input.anyKeyDown)
        {
            gun.Ready();
        }
    }
    public void OnReloadComplete()
    {
        Debug.Log("Reload Complete");
        AddAmmo();
        UnlockShooting();
    }
    public void OnSelectedGun()
    {      
        UpdateShootLocking();
    }
    private void UpdateShootLocking()
    {
        gun.enabled = _loadedAmmo > 0;
    }
    public void InitializeGun()
    {
        var newGun = GunManager.Instance.GetGun(gunName);
        this.gun.gunData = newGun;
        magSize = newGun.gunStats.ammoCapacity;
        ReFillAmmo();
    }


    //public Gun gun;
    //private int _loadedAmmo;
    //public bool isReloadComplete;
    //public UnityEvent <Gun>loadedAmmoChanged;

    //public int LoadedAmmo
    //{
    //    get => _loadedAmmo;
    //    set
    //    {
    //        _loadedAmmo = value;
    //        loadedAmmoChanged.Invoke(gun);
    //        if (_loadedAmmo <= 0)
    //        {
    //            LockShooting();
    //        }
    //        else
    //        {
    //            UnlockShooting();
    //        }
    //    }
    //}

    //public void Start()
    //{
    //    OnSelectedGun();
    //    gun.onSwitching.AddListener(OnSelectedGun);                    
    //}

    //public void SingleFireAmmoCounter()
    //{
    //    LoadedAmmo--;      
    //}
    //public void LockShooting()
    //{
    //    gun.enabled = false;
    //}
    //public void UnlockShooting()
    //{
    //    gun.enabled = true;
    //}
    //public void ReFillAmmo()
    //{
    //    LoadedAmmo = gun.gunData.AmmoCapacity;       
    //}

    //public void OnCancelReload()
    //{
    //    gun.anim.SetTrigger("Ready");
    //    isReloadComplete = false;
    //}
    //public void OnReloadComplete()
    //{
    //    Debug.Log("Reload Complete");
    //    isReloadComplete = true;
    //}
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        if (LoadedAmmo >= gun.gunData.AmmoCapacity)
    //        {
    //            return;
    //        }
    //        else
    //        {      
    //            StartCoroutine(Reload());              
    //        }
    //    }
    //}
    //public IEnumerator Reload()
    //{              
    //    gun.ReloadState();
    //    LockShooting();
    //    float animationLength = gun.ReturnReloadTimes();
    //    if (Input.anyKeyDown)
    //    {
    //        OnCancelReload();
    //    }
    //    yield return new WaitForSeconds(animationLength);
    //    AddAmmo();
    //    isReloadComplete = false;
    //}
    //public void AddAmmo()
    //{
    //    ReFillAmmo();
    //}

    //public void OnSelectedGun()
    //{
    //    InitializeGun();
    //    UpdateShootLocking();
    //}
    //public void UpdateShootLocking()
    //{
    //    gun.enabled = _loadedAmmo > 0;    
    //}
    //private void InitializeGun( )
    //{
    //    Gun newGun = GunManager.Instance.FindActiveGun();
    //    gun = newGun;
    //    ReFillAmmo();      
    //}
}
