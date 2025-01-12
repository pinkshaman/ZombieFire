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
    public void LockShooting()
    {
        gun.enabled = false;
    }
    public void UnlockShooting()
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
    public void OnDisable()
    {
        gun.ResetAnimation();       
    }
    public void OnEnable()
    {
        UpdateShootLocking();
    }
    public void OnReloadComplete()
    {
        Debug.Log("Reload Complete");
        AddAmmo();
        UnlockShooting();

    }
    public void OnSelectedGun()
    {
        gun.Ready();
        UpdateShootLocking();
        InitializeGun();
    }
    private void UpdateShootLocking()
    {
        gun.enabled = LoadedAmmo > 0;
        gun.Idle();
    }
    public void InitializeGun()
    {
        var newGun = GunManager.Instance.GetGun(gunName);
        this.gun.gunData = newGun;
        magSize = newGun.gunStats.ammoCapacity;
        ReFillAmmo();
    }



}
