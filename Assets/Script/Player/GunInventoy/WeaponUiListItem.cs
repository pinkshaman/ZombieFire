using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUiListItem : MonoBehaviour
{
    public Image gunImage;
    public Text gunName;
    public WeaponUI weaponUI;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject isLocked;
    public int slotIndex;
    public void Start()
    {
        weaponUI = FindObjectOfType<WeaponUI>();
    }

    public void SetDataUiListItem(BaseGun baseGun, PlayerGun progess, GunSlot gunSlot)
    {
        gunImage.sprite = baseGun.gunModel.gunSprite;
        gunName.text = baseGun.GunName;

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

        }
        isLocked.SetActive(!progess.isUnlocked);
        if (isLocked)
        {
            slotIndex = 3;
        }
        else
        {
            slotIndex = 4;
        }
    }

    public void SetDataOnclick()
    {

    }
}


