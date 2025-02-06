using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSlotUi : MonoBehaviour
{
    public Image gunImage;
    public Text gunName;
    public Text gunAmmo;
    public Button changeButton;

   
    public void SetGunSlotUi(BaseGun baseGun, PlayerGun playerGun)
    {
        gunImage.sprite = baseGun.gunModel.gunSprite;
        gunImage.SetNativeSize();
        gunName.text = baseGun.GunName;
        gunAmmo.text = $"{baseGun.gunStats.ammoCapacity}/{playerGun.ammoStoraged}";
    }

}
