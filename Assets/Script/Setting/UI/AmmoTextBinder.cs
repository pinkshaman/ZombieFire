using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextBinder : MonoBehaviour
{
    public Text loadedTextAmmo;
    public Text gunName;
    public GunAmmo gunAmmo;


    private void Start()
    {       
        
        gunAmmo.loadedAmmoChanged.AddListener(UpdateGunAmmo);  
        UpdateGunAmmo();
    }

    public void UpdateGunAmmo()
    {
        loadedTextAmmo.text = $"{gunAmmo.LoadedAmmo}/{gunAmmo.magSize}";
        gunName.text = gunAmmo.gunName;
    }
    public void OnEnable()
    {
        UpdateGunAmmo();
    }

}
