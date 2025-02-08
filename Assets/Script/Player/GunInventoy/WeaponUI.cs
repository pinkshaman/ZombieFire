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

    public List<GameObject> starUI;
    public float power;
    public float critical;
    public float fireRate;
    public float reload;

    public GameObject gunModel;



    public void Start()
    {

    }

    public void SetDataUI(BaseGun baseGun, PlayerGun gun)
    {
        weaponName.text = baseGun.GunName;
        ammoAmount.text = $"{baseGun.gunStats.ammoCapacity}/{gun.ammoStoraged}";
        gunDecription.text = baseGun.gunModel.gunDecription;
        ammoPrice.text = baseGun.buyGun.ammoPrice.ToString();
    }
    public void UpgradePrice(int price)
    {
        upgradePrice.text = price.ToString();

    }
    public void UpdateStar(int star)
    {

    }
    public void UpdateAmmo(BaseGun baseGun,int ammo)
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

    public void EquipButtonClick()
    {

    }
   

}
