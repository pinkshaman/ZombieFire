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
        pistolGun.anim.Play("Fire", layer: -1, normalizedTime: 0);
        pistolGun.gunRayCaster.PerformRayCasting();
        audioSource.clip = pistolGun.gunAudio.Shooting;
        audioSource.Play();
        OnShooting.Invoke();
    }
    public override void ReLoading()
    {
        pistolGun.anim.SetTrigger("Reload");
        audioSource.clip = pistolGun.gunAudio.Reload;
        audioSource.Play();
    }

    public override void Hiding()
    {
        pistolGun.anim.Play("Hide");
        audioSource.clip = pistolGun.gunAudio.Hide;
        audioSource.Play();
    }

    public override void Ready()
    {
        pistolGun.anim.SetTrigger("Ready");
        audioSource.clip = pistolGun.gunAudio.Ready;
        audioSource.Play();
    }

}
