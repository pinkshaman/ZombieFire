using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour
{
    private int magSize;
    public Gun gun;
    public Text ShowUIMag;
    private int _loadedAmmo;
    public AudioSource reloadSound;
    public bool isReloadComplete;
    public int LoadedAmmo
    {
        get => _loadedAmmo;
        set
        {
            _loadedAmmo = value;
            if (_loadedAmmo <= 0)
            {
                LockShooting();
                StartCoroutine(Reload());
            }
            else
            {
                UnlockShooting();
            }
        }
    }

    private void Start()
    {
        magSize = gun.gunData.AmmoCapacity;
        ReFillAmmo();
        ShowUi();
    }
    public void SingleFireAmmoCounter()
    {
        LoadedAmmo--;
        ShowUi();
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
        ShowUi();
    }
    public void ShowUi()
    {
        ShowUIMag.text = $"{LoadedAmmo}/{magSize}";
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (LoadedAmmo == magSize)
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
        if (Input.anyKeyDown)
        {
            OnCancelReload();
        }
        yield return new WaitUntil(() => isReloadComplete);
        AddAmmo();
        isReloadComplete = false;
    }
    public void AddAmmo()
    {
        ReFillAmmo();
    }
    public void OnCancelReload()
    {
        gun.ReadyState();
        isReloadComplete = false;
    }
    public void OnReloadComplete()
    {
        Debug.Log("Reload Complete");
        isReloadComplete = true;
    }
    public void OnSelectedGun()
    {
        UpdateShootLocking();
    }
    public void UpdateShootLocking()
    {
        gun.enabled = _loadedAmmo > 0;
        ShowUi();
    }
}
