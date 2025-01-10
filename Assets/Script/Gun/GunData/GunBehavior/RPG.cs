using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RPG", menuName = "Gun/RPG", order =2)]
public class RPG : ScriptableObject
{
    [Header("RPG Specific Properties")]
    public string gunName;
    public GameObject bulletPrefabs;
    public GunAudio gunAudio;
    public Transform firingPos;
    public GameObject rocket; 
    public ParticleSystem muzzleSmoke;
    //private float reloadSpeed;
    //public bool isReloading = false;

  
   // public void Update()
   // {

   //     if (Input.GetMouseButtonDown(0) && !isReloading)
   //     {
   //         StartCoroutine(ShootBullet());
   //     }
   // }
   //public void DeavtiveRocket()
   // {
   //     rocket.gameObject.SetActive(false);
   // }

   // public IEnumerator ShootBullet()
   // {
   //     ammo.SingleFireAmmoCounter();
   //     AddProjectile();
        
   //     muzzleSmoke.Play();
   //     ammo.LockShooting();
   //     yield return new WaitForSeconds(0.5f);
   //     StartCoroutine(ReLoadRocket());

   // }
   // public IEnumerator ReLoadRocket()
   // {
   //     if (ammo.LoadedAmmo > 0)
   //     {
   //         isReloading = true;
            
            
   //         yield return new WaitForSeconds(reloadSpeed);
   //         isReloading = false;
   //         ammo.UnlockShooting();
   //     }
       
   // }

   // public void AddProjectile()
   // {
   //     GameObject bullet = Instantiate(bulletPrefabs, firingPos.position, firingPos.rotation);
   //     bullet.GetComponent<Rigidbody>().velocity = firingPos.forward * bulletSpeed;
    //}
}
