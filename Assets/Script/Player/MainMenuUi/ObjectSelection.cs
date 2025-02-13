using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour
{
    public List<GameObject> objects;
    public Button upgradeButton;
    public Text title;
    public Text decription;
    public Text price;
    public Image priceImage;
    public UnityEvent CanUpgadeEffect;
    private GameObject selectedObject;
    private bool _canUpgrade;
    public bool CanUpgrade
    {
        get => _canUpgrade;
        set
        {
            _canUpgrade = value;
            CanUpgadeEffect.Invoke();
        }
    }

    public void Start()
    {
        CanUpgadeEffect.AddListener(UpdateUpgradeButton);
        upgradeButton.onClick.AddListener(UpgradeSelectedObject);
        for (int i = 0; i < objects.Count; i++)
        {
            int index = i;
            Button btn = objects[i].AddComponent<Button>();
            btn.onClick.AddListener(() => SelectObject(index));       
        }
        SelectObject(0);
    }
    public void SelectObject(int index)
    {
        GameObject obj = objects[index];
        if (selectedObject == obj) return;
        if (selectedObject != null)
        {
            selectedObject.GetComponent<SpecialUpgrades>().checkMark.SetActive(false);
            selectedObject.GetComponent<SpecialUpgrades>().anim.Stop();
        }
        selectedObject = obj;
        if (selectedObject.GetComponent<SpecialUpgrades>().specialType== SpecialType.Gear)
        {
            GearUpgrade effect = selectedObject.GetComponent<GearUpgrade>();
            effect.checkMark.SetActive(true);
            effect.PlayAnim();
            price.text = effect.ReturnPrice().ToString();
            priceImage.sprite = effect.data.priceImage;
            title.text = effect.data.Title.ToString();
            decription.text = $"{effect.data.Decription} {effect.ReturnInfor()} %";
            CanUpgrade = effect.CanUpgrade();
        }
        else
        {
            ItemUpgrade item = selectedObject.GetComponent<ItemUpgrade>();
            item.checkMark.SetActive(true);
            item.PlayAnim();
            price.text = item.ReturnPrice().ToString();
            title.text = item.data.itemName;
            decription.text = $"{item.data.decription}";
            priceImage.sprite = item.data.price.priceImage;
            CanUpgrade = item.IsCanBuy();
        }

    }

    public void UpgradeSelectedObject()
    {
        if (selectedObject.GetComponent<SpecialUpgrades>().specialType == SpecialType.Gear)
        {
            GearUpgrade effect = selectedObject.GetComponent<GearUpgrade>();
            if (effect.CanUpgrade())
            {
                effect.Upgrade();
                CanUpgrade = effect.CanUpgrade();
                price.text = effect.ReturnPrice().ToString();
                decription.text = $"{effect.data.Decription} {effect.ReturnInfor()} % ";
            }
        }
        else
        {
            ItemUpgrade item = selectedObject.GetComponent<ItemUpgrade>();
            if(item.IsCanBuy())
            {
                item.BuyItem();
                CanUpgrade = item.IsCanBuy();
            }
        }
    }
    private void UpdateUpgradeButton()
    {
        upgradeButton.interactable = CanUpgrade;
    }

}
