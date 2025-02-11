using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Text weaponName;
    public Text unlockRequire;
    public Text ammoAmount;
    public Text ammoPrice;
    public Image ammoPriceImage;
    public Button buyAmmoButton;
    public Button UpgradeButton;
    public Text upgradePrice;
    public Image priceImage;
    public Button equipButton;
    public Button buyGunButton;
    public Text buyGunPrice;
    public Image buyGunPriceImage;
    public Text gunDecription;

    public GameObject EquipPanel;
    public List<GameObject> starUI;
    public Image powerBar;
    public Image powerFillBar;
    public Image crittcalBar;
    public Image crittcalFillBar;
    public Image fireRateBar;
    public Image fireRateFillBar;
    public Image reloadBar;
    public Image reloadFillBar;
    public Text detailDamage;
    public Text detailCriticalDamage;
    private int PowerTotal = 200;
    private int currenDamage = 0;
    private float CriticalTotal = 100;
    private float currenCrit = 0;
    private float currentFirate = 0;
    private int FireRateTotal = 2000;
    private float reloadTotal = 10;
    private float currentReload = 0;
    private object gameobject;

    public void SetDataUI(BaseGun baseGun, PlayerGun gun)
    {
        weaponName.text = baseGun.GunName;
        ammoAmount.text = $"{baseGun.gunStats.ammoCapacity}/{gun.ammoStoraged}";
        gunDecription.text = baseGun.gunModel.gunDecription;
        ammoPrice.text = baseGun.buyGun.ammoPrice.ToString();
        UpdateStar(gun.starUpgrade);
        UpdataDataPowerUI(baseGun, gun);

    }
    public void UpdataDataPowerUI(BaseGun baseGun, PlayerGun progess)
    {
        PowerTotal = 200;
        currenDamage = baseGun.gunStats.damage;
        CriticalTotal = 100;
        currenCrit = baseGun.gunStats.critical;
        currentFirate = baseGun.gunStats.fireRate;
        FireRateTotal = 2000;
        reloadTotal = 10;
        currentReload = GunManager.Instance.ReturnReloadTimes(baseGun.GunName);

        foreach (var leveUpgrade in baseGun.upgradeList.gunUgradeList)
        {
            if (leveUpgrade.starUpgrade > progess.starUpgrade) break;
            currenDamage += leveUpgrade.powerUpgrade;
            currenCrit += leveUpgrade.criticalUpgrade;
            currentFirate += leveUpgrade.fireRateUpgrade;
        }

        powerFillBar.fillAmount = (float)currenDamage / PowerTotal;
        detailDamage.text = $"+{currenDamage}";
        crittcalFillBar.fillAmount = currenCrit / CriticalTotal;
        detailCriticalDamage.text = $"+{currenCrit}";
        fireRateFillBar.fillAmount = currentFirate / FireRateTotal;
        reloadFillBar.fillAmount = (currentReload / reloadTotal);
    }
    public void ResetDataPower()
    {
        PowerTotal = 200;
        currenDamage = 0;
        CriticalTotal = 100;
        currenCrit = 0;
        currentFirate = 0;
        FireRateTotal = 2000;
        reloadTotal = 10;
        currentReload = 0;
    }
    public void UpgradePrice(int price)
    {
        upgradePrice.text = price.ToString();
    }
    public void UpDatePriceToBuyGun(Price price)
    {
        buyGunPrice.text = price.cost.ToString();
        buyGunPriceImage.sprite = price.priceImage;
        buyGunPriceImage.SetNativeSize();
    }
    public void UpdateStar(int star)
    {
        for (int i = 0; i < starUI.Count; i++)
        {
            
            int starUpdate = star ;
            starUI[i].gameObject.SetActive(i < starUpdate);

        }
    }
    public void UpdateAmmo(BaseGun baseGun, int ammo)
    {
        ammoAmount.text = $"{baseGun.gunStats.ammoCapacity}/{ammo}";
    }
    public void UpdateBuyButtonUi(bool isCanBuy)
    {
        buyGunButton.interactable = isCanBuy;

    }
    public void UpdateUpgradeButonUI(bool isCanUpgrade)
    {
        UpgradeButton.interactable = isCanUpgrade;

    }
    public void UpdateBuyAmmoButtonUI(bool isCanBuyAmmo)
    {
        buyAmmoButton.interactable = isCanBuyAmmo;
    }

    public void ActiveEquipPanel()
    {
        EquipPanel.SetActive(true);
    }


}
