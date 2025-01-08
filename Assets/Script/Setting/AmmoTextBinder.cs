using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextBinder : MonoBehaviour
{
    public Text loadedTextAmmo;
    public Text gunName;
    public Gun gun;

  
    private void Start()
    {
        gun = FindObjectOfType<Gun>();
        UpdateGunAmmo();
        gun.gunAmmo.loadedAmmoChanged.AddListener(UpdateGunAmmo);    
    }

    public void UpdateGunAmmo()
    {
        loadedTextAmmo.text = $"{gun.gunAmmo.LoadedAmmo}/{gun.gunAmmo.gun.gunData.AmmoCapacity}";
        gunName.text = gun.gunAmmo.gun.gunName;
    }
}
