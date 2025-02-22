
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour
{
    public string gunName;
    public Gun gun;
    public int magSize;
    private int _loadedAmmo;
    public GamePlayUI gamePlayUI;
    public UnityEvent onBuyAmmo;
    public UnityEvent loadedAmmoChanged;
    private int cost;
    private bool isReloading = false;

    public AmmoTextBinder ammoTextUI;
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
        gamePlayUI = FindFirstObjectByType<GamePlayUI>();
        ammoTextUI = FindObjectOfType<AmmoTextBinder>();
        InitializeGun();
        UpdateTextAmmo();
        ammoTextUI.buyAmmo.onClick.AddListener(() => AutoBuy());
        loadedAmmoChanged.AddListener(UpdateTextAmmo);
        gun.OnShooting.AddListener(SingleFireAmmoCounter);
        gun.OnSwitching.AddListener(OnSelectedGun);
        gamePlayUI.reloadButton.onClick.AddListener(IsCanReload);
        cost = gun.gunData.buyGun.ammoPrice;

    }
    public void UpdateTextAmmo()
    {
        ammoTextUI.UpdateGunAmmo(LoadedAmmo,gun.gunPlayer.ammoStoraged,gun.GunName);
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
        int ammoToReload = Mathf.Min(magSize - LoadedAmmo, gun.gunPlayer.ammoStoraged);
        if (ammoToReload <= 0 && AutoBuy())
        {
            ammoToReload = Mathf.Min(magSize - LoadedAmmo, gun.gunPlayer.ammoStoraged);
        }
        ammoToReload = Mathf.Min(ammoToReload, gun.gunPlayer.ammoStoraged);
        if (ammoToReload > 0)
        {
            gun.gunPlayer.ammoStoraged -= ammoToReload;
            LoadedAmmo += ammoToReload;
            GunManager.Instance.UpdateAmmo(gun.gunData.GunName, gun.gunPlayer.ammoStoraged);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && LoadedAmmo < magSize)
        {
            Reload();
        }

    }
    public void IsCanReload()
    {
        if(LoadedAmmo < magSize)
        {
            Reload();
        }
    }
    public void Reload()
    {
        if (gun.gunPlayer.ammoStoraged <= 0 && !AutoBuy() || isReloading)
        {
            return;
        }
        isReloading = true;
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
        isReloading = false;
    }
    public void OnSelectedGun()
    {
        gun.ResetAnimation();
        UpdateTextAmmo();
        UpdateShootLocking();
    }
    private void UpdateShootLocking()
    {
        gun.enabled = LoadedAmmo > 0;
        isReloading = false;
    }
    public void InitializeGun()
    {
        var newGun = GunManager.Instance.GetGun(gunName);
        this.gun.gunData = newGun;
        magSize = newGun.gunStats.ammoCapacity;
        ReFillAmmo();
    }
    public bool AutoBuy()
    {
        if (PlayerManager.Instance.playerData.coin >= cost)
        {
            PlayerManager.Instance.playerData.coin -= cost;
            PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);
            gun.gunPlayer.ammoStoraged += gun.gunData.gunStats.ammoCapacity;
            GunManager.Instance.UpdateAmmo(gun.gunData.GunName, gun.gunPlayer.ammoStoraged);
            ammoTextUI.IsBuyAmmo(cost);
            onBuyAmmo.Invoke();
            return true;
        }
        return false;
    }



}
