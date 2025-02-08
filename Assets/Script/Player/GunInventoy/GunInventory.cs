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

    private WeaponUiListItem selectedGunObject;

    private int currenPrice;
    public void Start()
    {
        GunManager.Instance.CreateWeaponInventory();
        SortGunUI();
        AddGunButton();
        SelectGun(0);
    }
    public void CreateGunUI(BaseGun baseGun, PlayerGun playerGun, GunSlot gunSlot)
    {
        var gun = Instantiate(weaponPrefabs, rootUi);
        gun.SetDataUiListItem(baseGun, playerGun, gunSlot);
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
    public void AddGunButton()
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            int index = i;
            Button btn = listItems[i].GetComponent<Button>();
            btn.onClick.AddListener(() => SelectGun(index));
        }
    }
    private void SelectGun(int index)
    {
        WeaponUiListItem obj = listItems[index];
        if (selectedGunObject == obj) return;
        if (selectedGunObject != null)
        {
            Debug.Log($"Unsellected: {selectedGunObject.baseGun.GunName}");
            RemoveListenerMethod();
        }
        selectedGunObject = obj;
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

    }
    public void UpdateBuyButton()
    { 

    }
    public void UpdateBuyAmmoButton()
    {

    }

    public void AddListenerMethod()
    {
        Debug.Log($"Added Listener:{selectedGunObject.gunName}");
        selectedGunObject.selectedObject.SetActive(true);

        weaponUI.buyGunButton.onClick.AddListener(() => selectedGunObject.BuyGun());
        weaponUI.UpgradeButton.onClick.AddListener(() => selectedGunObject.Upgrade());
        weaponUI.buyAmmoButton.onClick.AddListener(() => selectedGunObject.BuyAmmo());

    }
    public void RemoveListenerMethod()
    {
        Debug.Log($"Removed Listener: {selectedGunObject.gunName}");
        //weaponUI.buyGunButton.onClick.RemoveListener(() => selectedGunObject.BuyGun());
        //weaponUI.UpgradeButton.onClick.RemoveListener(() => selectedGunObject.Upgrade());
        //weaponUI.buyAmmoButton.onClick.RemoveListener(() => selectedGunObject.BuyAmmo());
        selectedGunObject.selectedObject.SetActive(false);
    }
    public void UpgradeSelectedGun()
    {

        var currentStar = selectedGunObject.ReturnCurrenStar();
        int upgradeCost = selectedGunObject.ReturnPriceUpgrade();
        selectedGunObject.Upgrade();
        ShowGunUiOnClick(selectedGunObject);

    }
    public void BuySelectedGunAmmo()
    {
        if (selectedGunObject.IsCanBuyGunAmmo())
        {
            ShowGunUiOnClick(selectedGunObject);
        }

    }
    public void BuySeclectedGun()
    {
        selectedGunObject.BuyGun();
        weaponUI.equipButton.gameObject.SetActive(selectedGunObject.playerGun.isUnlocked);
        ShowGunUiOnClick(selectedGunObject);
    }


}

