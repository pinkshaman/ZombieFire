using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RpgGun : Gun
{
    public RPG rpgGun;
    private float reloadSpeed;
    public float bulletSpeed;
    public bool isReloading = false;

    public void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Shooting();
        }
    }
    public void DeavtiveRocket()
    {
        rpgGun.rocket.gameObject.SetActive(false);
    }

    public IEnumerator ShootBullet()
    {
        gunAmmo.SingleFireAmmoCounter();
        AddProjectile();
        anim.Play("Fire");
        audioSource.clip = rpgGun.gunAudio.Shooting;
        audioSource.Play();
        rpgGun.muzzleSmoke.Play();
        gunAmmo.LockShooting();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ReLoadRocket());

    }
    public IEnumerator ReLoadRocket()
    {
        if (gunAmmo.LoadedAmmo > 0)
        {
            isReloading = true;
            reloadSpeed = 1.5f;

            yield return new WaitForSeconds(reloadSpeed);
            isReloading = false;
            gunAmmo.UnlockShooting();
        }

    }

    public void AddProjectile()
    {
        GameObject bullet = Instantiate(rpgGun.bulletPrefabs, rpgGun.firingPos.position, rpgGun.firingPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = rpgGun.firingPos.forward * bulletSpeed;
    }
    public override void Shooting()
    {
        
        StartCoroutine(ShootBullet());
    }

    public override void ReLoading()
    {
        StartCoroutine(ReLoadRocket());
    }
}
