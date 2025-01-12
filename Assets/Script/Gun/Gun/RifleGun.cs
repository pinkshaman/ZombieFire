using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class RifleGun : Gun
{
    public Rifle rifleGun;
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
        rifleGun.gunRayCaster.PerformRayCasting();
        audioSource.clip = rifleGun.gunAudio.Shooting;
        audioSource.Play();
        rifleGun.shellBullet.Play();
        OnShooting.Invoke();
    }
    public override void ReLoading()
    {
        anim.SetTrigger("Reload");
        audioSource.clip = rifleGun.gunAudio.Reload;
        audioSource.Play();
        OnReloading.Invoke();
    }

    public override void Hiding()
    {    
        base.Hiding();
        audioSource.clip = rifleGun.gunAudio.Hide;
        audioSource.Play();

    }

    public override void Ready()
    {
        base.Ready();
        audioSource.clip = rifleGun.gunAudio.Ready;
        audioSource.Play();
    }
  
 
}
