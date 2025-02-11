using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponUiListItem : MonoBehaviour
{
    public Image gunImage;
    public Text gunName;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject isLocked;
    public GameObject selectedObject;
    public int slotIndex;
    public Button selectButton;

    public BaseGun baseGun;
    public PlayerGun playerGun;

    public UnityEvent OnAmmoChanged;
    private int currentStar;
    private int _currentAmmo;
    public int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = value;
            OnAmmoChanged.Invoke();
        }
    }

    private UnityAction<WeaponUiListItem> _onClickCallback;

    public void Start()
    {
        currentStar = playerGun.starUpgrade;
        CurrentAmmo = playerGun.ammoStoraged;
        selectButton.onClick.AddListener(() => _onClickCallback.Invoke(this));
    }
    public void SetDataUiListItem(BaseGun baseGun, PlayerGun progess, GunSlot gunSlot, UnityAction<WeaponUiListItem> onClickCallback)
    {
        this.baseGun = baseGun;
        this.playerGun = progess;
        gunImage.sprite = baseGun.gunModel.gunSprite;
        gunImage.SetNativeSize();
        gunName.text = baseGun.GunName;
        isLocked.SetActive(!progess.isUnlocked);
        _onClickCallback = onClickCallback;

        UpdateLabel(progess, gunSlot);
    }
    public void UpdateLabel(PlayerGun progess, GunSlot gunSlot)
    {
        if (!progess.isUnlocked)
        {
            slotIndex = 4;
            Slot1.SetActive(false);
            Slot2.SetActive(false);
            return;
        }
        if (gunName.text == gunSlot.gunSlot1)
        {
            Slot1.SetActive(true);
            Slot2.SetActive(false);
            slotIndex = 1;
        }
        else if (gunName.text == gunSlot.gunSlot2)
        {
            Slot2.SetActive(true);
            Slot1.SetActive(false);
            slotIndex = 2;
        }
        else
        {
            Slot1.SetActive(false);
            Slot2.SetActive(false);
            slotIndex = 3;
        }
    }
    public Price ReturnPriceToBuy()
    {
        var price = baseGun.buyGun.price;
        return price;

    }
    public int ReturnPriceUpgrade()
    {
        if (currentStar + 1 >= baseGun.upgradeList.gunUgradeList.Count) return 0;
        int upgradeCost = baseGun.upgradeList.gunUgradeList[currentStar + 1].priceUpgrade;
        return upgradeCost;
    }
    public bool IsCanBuyGun()
    {
        if (playerGun.isUnlocked) return false;
        if (baseGun.buyGun.price.priceType == PriceType.Coin)
        {

            return PlayerManager.Instance.playerData.coin >= baseGun.buyGun.price.cost;
        }
        else
        {
            return PlayerManager.Instance.playerData.gold >= baseGun.buyGun.price.cost;
        }
    }
    public bool CanUpgradeWeapon()
    {
        if (!playerGun.isUnlocked) return false;
        if (currentStar + 1 >= baseGun.upgradeList.gunUgradeList.Count) return false;

        int upgradeCost = baseGun.upgradeList.gunUgradeList[currentStar + 1].priceUpgrade;
        return PlayerManager.Instance.playerData.coin >= upgradeCost;
    }
    public void Upgrade()
    {
        if (!CanUpgradeWeapon()) return;
        int upgradeCost = baseGun.upgradeList.gunUgradeList[currentStar + 1].priceUpgrade;
        PlayerManager.Instance.playerData.coin -= upgradeCost;
        PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);
        currentStar++;
        playerGun.starUpgrade = currentStar;
        GunManager.Instance.UpdateGunData(playerGun.gunName, playerGun);
        Debug.Log($"Upgrade : {baseGun.GunName} -{playerGun.starUpgrade}");
    }
    public int ReturnCurrenStar()
    {
        return currentStar;
    }
    public void BuyGun()
    {
        if (playerGun.isUnlocked) return;
        if (baseGun.buyGun.price.priceType == PriceType.Coin)
        {
            PlayerManager.Instance.playerData.coin -= baseGun.buyGun.price.cost;
            PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);
        }
        else
        {
            PlayerManager.Instance.playerData.gold -= baseGun.buyGun.price.cost;
            PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);
        }
        playerGun.isUnlocked = true;
        GunManager.Instance.UpdateGunData(playerGun.gunName, playerGun);
        Debug.Log($"BuyGun: {baseGun.GunName}");
    }
    public bool IsCanBuyGunAmmo()
    {
        if (!playerGun.isUnlocked) return false;
        int Cost = baseGun.buyGun.ammoPrice;
        return PlayerManager.Instance.playerData.coin >= Cost;
    }
    public void BuyAmmo()
    {
        int Cost = baseGun.buyGun.ammoPrice;
        if (PlayerManager.Instance.playerData.coin >= Cost)
        {
            PlayerManager.Instance.playerData.coin -= Cost;
            PlayerManager.Instance.UpdatePlayerData(PlayerManager.Instance.playerData);


            CurrentAmmo += baseGun.gunStats.ammoCapacity;
            GunManager.Instance.UpdateAmmo(baseGun.GunName, CurrentAmmo);

            Debug.Log($"CurrenAmmo:{playerGun.ammoStoraged}");
        }
        else
        {
            Debug.Log("Not Enough Money!");
        }

    }
}


