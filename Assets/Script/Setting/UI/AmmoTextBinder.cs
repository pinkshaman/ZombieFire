using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AmmoTextBinder : MonoBehaviour
{
    public Text loadedTextAmmo;
    public Text gunName;
    public Button buyAmmo;
    public BuyText buyText;

 
    public void UpdateGunAmmo(int loadedAmmo, int ammoStoraged, string name)
    {
        loadedTextAmmo.text = $"{loadedAmmo}/{ammoStoraged}";
        gunName.text = name;
    }
  
    [ContextMenu("IsBuyAmmo")]
    public void IsBuyAmmo(int cost)
    {
        buyText.gameObject.SetActive(true);
        buyText.SetText(cost.ToString());
    }
}
