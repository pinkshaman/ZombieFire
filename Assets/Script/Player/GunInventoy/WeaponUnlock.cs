using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlock : MonoBehaviour
{
    public Text gunName;
    public Image gunImage;
    public Button touchToExit;
    public void SetData(WeaponUiListItem weapon)
    {
        gunName.text = weapon.baseGun.GunName.ToString();
        gunImage.sprite = weapon.baseGun.gunModel.gunSprite;
        gunImage.SetNativeSize();
    }
    public void TouchToExit()
    {
        gameObject.SetActive(false);
    }
}
