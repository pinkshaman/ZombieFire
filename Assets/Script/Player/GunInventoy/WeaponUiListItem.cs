using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUiListItem : MonoBehaviour
{
    public LabelList labelList;
    public Image gunImage;
    public Text gunName;
    public WeaponUI weaponUI;
    private LabelActive selectedLabel;
    public void Start()
    {
        weaponUI = FindObjectOfType<WeaponUI>();
    }
    public void OnEnable()
    {
        GunManager.Instance.CreateWeaponInventory();
    }
    public void SetDataUiListItem(BaseGun baseGun, PlayerGun progess, GunSlot gunSlot)
    {
        gunImage.sprite = baseGun.gunModel.gunSprite;
        gunName.text = baseGun.GunName;
       

        if (gunName.text == gunSlot.gunSlot1)
        {
            selectedLabel = LabelActive.Slot1;
        }
        else if (gunName.text == gunSlot.gunSlot2)
        {
            selectedLabel = LabelActive.Slot2;
        }
        else
        {
            selectedLabel = LabelActive.Locked;
        }
        labelList.SetActiveLabel(selectedLabel, progess.isUnlocked);
        weaponUI.SetData(baseGun, progess);
        
    }
 
    public void SetDataOnclick()
    {
        
    }
}

[Serializable]
public class Label
{
    public LabelActive label;
    public GameObject labelObject;
}
[Serializable]
public class LabelList
{
    public List<Label> labelLists;

    public void SetActiveLabel(LabelActive activeLabel, bool isUnlocked)
    {
        foreach (var item in labelLists)
        {
            if (item.label == activeLabel)
            {
                item.labelObject.SetActive(true);
            }
            else
            {
                item.labelObject.SetActive(false);
            }
        }

        if (activeLabel == LabelActive.Locked)
        {
            foreach (var item in labelLists)
            {
                item.labelObject.SetActive(!isUnlocked);
            }
        }
    }
}
public enum LabelActive
{
    Slot1,
    Slot2,
    Locked,
}
