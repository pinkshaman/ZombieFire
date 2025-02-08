using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GunInventory : MonoBehaviour
{
    public Transform rootUi;
    public WeaponUiListItem weaponPrefabs;
    public WeaponUI weaponUI;
    public List<WeaponUiListItem> listItems = new List<WeaponUiListItem>();
    public WeaponChange weaponChange;

    private WeaponUiListItem selectedGunObject;
    private int currenPrice;

    public UnityEvent CanUpgadeWeapon;
    public UnityEvent CanBuyWeapon;
    public UnityEvent CanBuyAmmo;
    private bool _canUpgrade;
    public bool CanUpgrade
    {
        get => _canUpgrade;
        set
        {
            _canUpgrade = value;
            CanUpgadeWeapon.Invoke();
        }
    }
    public bool _isCanBuy;
    public bool IsCanBuy
    {
        get => _isCanBuy;
        set
        {
            _isCanBuy = value;
            CanBuyWeapon.Invoke();
        }
    }

    public bool _isCanBuyAmmo;
    public bool IsCanBuyAmmo
    {
        get => _isCanBuyAmmo;
        set
        {
            _isCanBuyAmmo = value;
            CanBuyAmmo.Invoke();
        }
    }
    public void Start()
    {
        GunManager.Instance.CreateWeaponInventory();
        SortGunUI();
        SelectGun(listItems[0]);
    }
    public void CreateGunUI(BaseGun baseGun, PlayerGun playerGun, GunSlot gunSlot)
    {
        var gun = Instantiate(weaponPrefabs, rootUi);
        gun.SetDataUiListItem(baseGun, playerGun, gunSlot, SelectGun);
        listItems.Add(gun);

    }
    [ContextMenu("SorGunUi")]
    public void SortGunUI()
    {
        listItems = listItems.OrderBy(gun => gun.slotIndex).ToList();

        for (int i = 0; i < listItems.Count; i++)
        {
            listItems[i].transform.SetSiblingIndex(i);
        }
    }
  
    public void SelectGun(WeaponUiListItem gubObject)
    {
        if (selectedGunObject == gubObject) return;
        if (selectedGunObject != null)
        {
            Debug.Log($"Unsellected: {selectedGunObject.baseGun.GunName}");
            RemoveListenerMethod();
        }
        selectedGunObject = gubObject;
        Debug.Log($"Sellected: {selectedGunObject.baseGun.GunName}");

        ShowGunUiOnClick(selectedGunObject);
        weaponUI.equipButton.gameObject.SetActive(selectedGunObject.playerGun.isUnlocked);
        AddListenerMethod();
    }
    public void ShowGunUiOnClick(WeaponUiListItem data)
    {
        var priceUpgrade = selectedGunObject.ReturnPriceUpgrade();
        int star = selectedGunObject.ReturnCurrenStar();
        weaponUI.SetDataUI(data.baseGun, data.playerGun);
        weaponUI.UpdateAmmo(data.baseGun, data.playerGun.ammoStoraged);
        weaponUI.UpgradePrice(priceUpgrade);
        weaponUI.UpdateStar(star);
    }
    public void UpdateUpgradeButton()
    {
        weaponUI.UpdateUpgradeButonUI(selectedGunObject.CanUpgradeWeapon());
    }
    public void UpdateBuyButton()
    { 
        weaponUI.UpdateBuyButtonUi(selectedGunObject.IsCanBuyGun());
    }
    public void UpdateBuyAmmoButton()
    {
        weaponUI.UpdateBuyAmmoButtonUI(selectedGunObject.IsCanBuyGunAmmo());
    }

    public void AddListenerMethod()
    {
        Debug.Log($"Added Listener:{selectedGunObject.gunName}");
        selectedGunObject.selectedObject.SetActive(true);
        weaponUI.buyGunButton.onClick.AddListener(BuySeclectedGun);
        weaponUI.UpgradeButton.onClick.AddListener(UpgradeSelectedGun);
        weaponUI.buyAmmoButton.onClick.AddListener(BuySelectedGunAmmo);
        weaponUI.equipButton.onClick.AddListener(weaponUI.ActiveEquipPanel);
        weaponUI.equipButton.onClick.AddListener(EquipButton);

        CanUpgadeWeapon.AddListener(UpdateUpgradeButton);
        CanBuyWeapon.AddListener(UpdateBuyButton);
        CanBuyAmmo.AddListener(UpdateBuyAmmoButton);
    }
    public void RemoveListenerMethod()
    {
        Debug.Log($"Removed Listener: {selectedGunObject.gunName}");
        selectedGunObject.selectedObject.SetActive(false);
        weaponUI.buyGunButton.onClick.RemoveListener(BuySeclectedGun);
        weaponUI.UpgradeButton.onClick.RemoveListener(UpgradeSelectedGun);
        weaponUI.buyAmmoButton.onClick.RemoveListener(BuySelectedGunAmmo);
        weaponUI.equipButton.onClick.AddListener(weaponUI.ActiveEquipPanel);
        weaponUI.equipButton.onClick.AddListener(EquipButton);

        CanUpgadeWeapon.RemoveListener(UpdateUpgradeButton);
        CanBuyWeapon.RemoveListener(UpdateBuyButton);
        CanBuyAmmo.RemoveListener(UpdateBuyAmmoButton);
    }
    public void UpgradeSelectedGun()
    {
        selectedGunObject.Upgrade();
        ShowGunUiOnClick(selectedGunObject);
        //CanUpgrade = selectedGunObject.CanUpgradeWeapon();
    }
    public void BuySelectedGunAmmo()
    {
        if (selectedGunObject.IsCanBuyGunAmmo())
        {
            selectedGunObject.BuyAmmo();
            ShowGunUiOnClick(selectedGunObject);
        }

    }
    public void BuySeclectedGun()
    {
        if (!IsCanBuy) return;
        selectedGunObject.BuyGun();
        weaponUI.equipButton.gameObject.SetActive(selectedGunObject.playerGun.isUnlocked);
        ShowGunUiOnClick(selectedGunObject);
    }
    public void EquipButton()
    {
        weaponChange.SetDataUi(selectedGunObject, GunManager.Instance.gunSlot);
    }

}

