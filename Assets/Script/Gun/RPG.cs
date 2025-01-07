using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RPG : Gun
{
    [Header("RPG Specific Properties")]
    public GameObject bulletPrefabs;
    public Transform firingPos;
    public float bulletSpeed;   
    public GunAmmo ammo;
    public GameObject RocketObj;   
    public ParticleSystem muzzleSmoke;

    public bool isReloading = false;
    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isReloading && ammo.LoadedAmmo>0)
        {     
            StartCoroutine(ShootBullet());          
        }    
    }

    public void ActiveRoket()
    {
        RocketObj.SetActive(true);
    }
    public IEnumerator ShootBullet()
    {       
        ammo.SingleFireAmmoCounter();
        ShootState();
        RocketObj.SetActive(false);
        muzzleSmoke.Play();
        AddProjectile();
        ammo.LockShooting();
        yield return new WaitForSeconds(0.5f);     
        StartCoroutine(ReLoadRocket());

    }
    public IEnumerator ReLoadRocket()
    {
        if (ammo.LoadedAmmo > 0)
        {
            
            isReloading = true;
            RocketObj.SetActive(true);
            ReloadState();           
            yield return new WaitForSeconds(gunData.ReloadTime);
            isReloading = false;
            ammo.UnlockShooting();
        }

    }
   
    public void AddProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefabs, firingPos.position, firingPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firingPos.forward * bulletSpeed;
    }
}
