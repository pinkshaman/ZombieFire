using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RPG : Gun
{
    public GameObject bulletPrefabs;
    public Transform firingPos;
    public float bulletSpeed;   
    public GunAmmo ammo;
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
            PlayFireSound();
        }
    }
    private void ShootBullet()
    {       
        AddProjectile();
        ammo.SingleFireAmmoCounter();
    }
    public void PlayFireSound()
    {
        ShootState();
    }
    public void AddProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefabs, firingPos.position, firingPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firingPos.forward * bulletSpeed;
    }
}
