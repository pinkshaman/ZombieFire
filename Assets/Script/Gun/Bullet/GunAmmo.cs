using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour
{   
    public Gun gun;  
    private int _loadedAmmo;
    public AudioSource reloadSound;
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

    public void Start()
    {      
        ReFillAmmo();     
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
    public void ReFillAmmo()
    {
        LoadedAmmo = gun.gunData.AmmoCapacity;       
    }
  
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (LoadedAmmo == gun.gunData.AmmoCapacity)
            {
                return;
            }
            else
            {
                StartCoroutine(Reload());              
            }
        }
    }
    public IEnumerator Reload()
    {

        gun.ReloadState();
        LockShooting();
        
        yield return new WaitForSeconds(gun.gunData.ReloadTime);
        AddAmmo();
        
    }
    public void AddAmmo()
    {
        ReFillAmmo();
    }
 
    public void OnSelectedGun()
    {
        UpdateShootLocking();
    }
    public void UpdateShootLocking()
    {
        gun.enabled = _loadedAmmo > 0;
        
    }
}
