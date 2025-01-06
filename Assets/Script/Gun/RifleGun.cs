using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class RifleGun : Gun
{
    
    [Header("Rifle Specific Properties")]   
    public GunRayCaster gunRayCaster;
    private float lastShoot;
    private float interval;

    public UnityEvent onShoot;
   
    public override void Start()
    {     
        base.Start();
        interval = 60 / gunData.FireRate;
    }
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateFiring();
            Debug.Log("Shoot");
        }
    }
    private void UpdateFiring()
    {
        interval = 60 / gunData.FireRate;
        if (Time.time - lastShoot >= interval)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }
    public void Shoot()
    {
        ShootState();
        gunRayCaster.PerformRayCasting();
        onShoot.Invoke();
    }
}
