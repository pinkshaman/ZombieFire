using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour
{
    public Text slot1Name;
    public Text slot2Name;
    public Text selectedWeapon;
    public Image slot1Image;
    public Image slot2Image;
    public Image selectedWeaponImage;
    public Button buttonSlot1;
    public Button buttonSlot2;

    public Button closePanel;
    public PlayerUI playerUi;
    public void Start()
    {
        buttonSlot1.onClick.AddListener(SetDataSlot1);
        buttonSlot2.onClick.AddListener(SetDataSlot2);
        closePanel.onClick.AddListener(ClosePanel);
    }
    public void SetDataUi(WeaponUiListItem data, GunSlot gunSlot)
    {
        slot1Name.text = gunSlot.gunSlot1;
        slot2Name.text = gunSlot.gunSlot2;

        var gunData1 = GunManager.Instance.GetGun(gunSlot.gunSlot1);
        slot1Image.sprite = gunData1.gunModel.gunSprite;

        var gunData2 = GunManager.Instance.GetGun(gunSlot.gunSlot2);
        slot2Image.sprite = gunData2.gunModel.gunSprite;

        selectedWeapon.text = data.baseGun.GunName;
        selectedWeaponImage.sprite = data.baseGun.gunModel.gunSprite;
        SetNativeImage(slot1Image, slot2Image, selectedWeaponImage);
    }
    public void SetNativeImage(Image image1, Image image2, Image selectIMG)
    {
        image1.SetNativeSize();
        image2.SetNativeSize();
        selectIMG.SetNativeSize();
    }
    public void SetDataSlot1()
    {
        if (slot1Name.text == selectedWeapon.text) return;
        if (slot2Name.text == selectedWeapon.text)
        {
            slot2Name.text = slot1Name.text;
            slot2Image.sprite = slot1Image.sprite; 
        }
        slot1Name.text = selectedWeapon.text;
        slot1Image.sprite = selectedWeaponImage.sprite;
        SetNativeImage(slot1Image, slot2Image, selectedWeaponImage);
        UpdateData(slot1Name.text, slot2Name.text);


    }
    public void SetDataSlot2()
    {
        if (slot2Name.text == selectedWeapon.text) return;
        if (slot1Name.text == selectedWeapon.text)
        {
            slot1Name.text = slot2Name.text;
            slot1Image.sprite = slot2Image.sprite;
        }
        slot2Name.text = selectedWeapon.text;
        slot2Image.sprite = selectedWeaponImage.sprite;
        SetNativeImage(slot1Image, slot2Image, selectedWeaponImage);
        UpdateData(slot1Name.text, slot2Name.text);

    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);

    }
    public void UpdateData(string slot1, string slot2)
    {
        GunManager.Instance.UpdateGunSlot(slot1,slot2);
        playerUi.UpdateSlotGun();
    }

}
