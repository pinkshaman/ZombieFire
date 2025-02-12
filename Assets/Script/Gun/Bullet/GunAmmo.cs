
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
    private int cost;
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
        cost = gun.gunData.buyGun.ammoPrice;
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
        LoadedAmmo += magSize;
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
        if (gun.gunPlayer.ammoStoraged <= 0 )
        {
            AutoBuy();
        }
        else
        {
            int ammoToReload = Mathf.Min(magSize - LoadedAmmo, gun.gunData.gunStats.ammoCapacity);
            if (gun.gunPlayer.ammoStoraged <= 0) return;
            LockShooting();
            gun.ReLoading();
            gun.gunPlayer.ammoStoraged -= ammoToReload;
            LoadedAmmo += ammoToReload;
            GunManager.Instance.UpdateAmmo(gun.gunData.GunName, gun.gunPlayer.ammoStoraged);
            
        }
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
    public void AutoBuy()
    {
        if (PlayerManager.Instance.playerData.coin >= cost)
        {
            PlayerManager.Instance.playerData.coin -= cost;
            PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);
            gun.gunPlayer.ammoStoraged += gun.gunData.gunStats.ammoCapacity;
            GunManager.Instance.UpdateAmmo(gun.gunData.GunName, gun.gunPlayer.ammoStoraged);
        }  
    }



}
