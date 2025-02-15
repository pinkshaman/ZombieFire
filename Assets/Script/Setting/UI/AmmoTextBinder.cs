using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextBinder : MonoBehaviour
{
    public Text loadedTextAmmo;
    public Text gunName;
    public GunAmmo gunAmmo;
    public Button buyAmmo;
    public BuyText buyText;
    private int cost;
     private void Start()
    {
        gunAmmo.loadedAmmoChanged.AddListener(UpdateGunAmmo);
        buyAmmo.onClick.AddListener(IsBuyAmmo);
        gunAmmo.onBuyAmmo.AddListener(IsBuyAmmo);
        UpdateGunAmmo();
        cost = gunAmmo.gun.gunData.buyGun.ammoPrice;
    }

    public void UpdateGunAmmo()
    {
        loadedTextAmmo.text = $"{gunAmmo.LoadedAmmo}/{gunAmmo.gun.gunPlayer.ammoStoraged}";
        gunName.text = gunAmmo.gunName;
    }
    public void OnEnable()
    {
        UpdateGunAmmo();
    }
    [ContextMenu("IsBuyAmmo")]
    public void IsBuyAmmo()
    {
        buyText.gameObject.SetActive(true);
        buyText.SetText(cost.ToString());
    }
}
