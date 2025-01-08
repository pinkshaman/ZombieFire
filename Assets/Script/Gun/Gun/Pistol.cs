using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public GunRayCaster gunRayCaster;
    private float lastShoot;
    private float interval;

    public override void Start()
    {
        base.Start();
        interval = 60 / gunData.FireRate;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateFiring();
            Debug.Log("Shoot");
        }
    }
    private void UpdateFiring()
    {

        if (Time.time - lastShoot >= interval)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }
    public void Shoot()
    {
        ShootState();
        gunAmmo.SingleFireAmmoCounter();
        gunRayCaster.PerformRayCasting();
        onShoot.Invoke();
    }
}
