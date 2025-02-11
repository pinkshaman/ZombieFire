using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGun : Gun
{
    public Pistol pistolGun;
    private float interval;
    private float lastShoot;


    public override void Start()
    {
        base.Start();
        interval = 60f / gunData.gunStats.fireRate;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateFiring();
        }
    }

    private void UpdateFiring()
    {

        if (Time.time - lastShoot >= interval)
        {
            Shooting();
            lastShoot = Time.time;
        }
    }

    public override void Shooting()
    {
        anim.Play("Fire", layer: -1, normalizedTime: 0);
        pistolGun.gunRayCaster.PerformRayCasting();
        audioSource.clip = pistolGun.gunAudio.Shooting;
        audioSource.Play();
        pistolGun.shellBullet.Play();
        OnShooting.Invoke();
    }
    public override void ReLoading()
    {
        audioSource.clip = pistolGun.gunAudio.Reload;
        base.ReLoading();
        audioSource.Play();
        OnReloading.Invoke();
    }

    public override void Hiding()
    {
        audioSource.clip = pistolGun.gunAudio.Hide;
        base.Hiding();
        audioSource.Play();
    }

    public override void Ready()
    {
        audioSource.clip = pistolGun.gunAudio.Ready;
        base.Ready();
        audioSource.Play();
    }
}

