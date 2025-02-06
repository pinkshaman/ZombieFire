using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    public Transform rootUi;
    public WeaponUiListItem weaponPrefabs;
    List<int> slotIndex = new List<int>();
    public void Start()
    {
        GunManager.Instance.CreateWeaponInventory();
    }
    public void CreateGunUI(BaseGun baseGun, PlayerGun playerGun, GunSlot gunSlot)
    {
        var gun = Instantiate(weaponPrefabs, rootUi);
        gun.SetDataUiListItem(baseGun, playerGun, gunSlot);
        slotIndex.Add(gun.slotIndex);
        slotIndex.Sort();
        
    }
}                                           
 
