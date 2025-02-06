using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Text weaponName;
    public Text unlockRequire;
    public Text starUpgrade;
    public Text ammoAmount;
    public Text ammoPrice;
    public Image ammoPriceImage;
    public Button buyAmmo;
    public Button UpgradeButton;
    public Text upgradePrice;
    public Image priceImage;
    public Button equipButton;
    public Button buyGunButton;
    public Text buyGunPrice;
    public Image buyGunPriceImage;

    public Text gunDecription;
    public float power;
    public float critical;
    public float fireRate;
    public float reload;

    public GameObject gunModel;


    public void SetData(BaseGun baseGun, PlayerGun gun)
    {
        weaponName.text = baseGun.GunName;
        ammoAmount.text = gun.ammoStoraged.ToString();
        gunDecription.text = baseGun.gunModel.gunDecription;
    }

}
