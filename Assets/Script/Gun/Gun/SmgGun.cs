using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmgGun : Gun
{
    public SMG smgGun;
    private float interval;
    private float lastShoot;


    public override void Start()
    {
        base.Start();
        interval = 60f / gunData.gunStats.fireRate;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
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
        smgGun.gunRayCaster.PerformRayCasting();
        audioSource.clip = smgGun.gunAudio.Shooting;
        audioSource.Play();
        smgGun.shellBullet.Play();
        OnShooting.Invoke();
    }
    public override void ReLoading()
    {
        anim.SetTrigger("Reload");
        audioSource.clip = smgGun.gunAudio.Reload;
        audioSource.Play();
        OnReloading.Invoke();
    }

    public override void Hiding()
    {
        base.Hiding();
        audioSource.clip = smgGun.gunAudio.Hide;
        audioSource.Play();

    }

    public override void Ready()
    {
        base.Ready();
        audioSource.clip = smgGun.gunAudio.Ready;
        audioSource.Play();
    }
}
