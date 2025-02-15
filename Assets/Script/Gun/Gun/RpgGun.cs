using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RpgGun : Gun
{
    public RPG rpgGun;
    private float reloadSpeed;
    public float bulletSpeed;
    public bool isRPGReloading = false;

    public  override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) && !isRPGReloading)
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
        anim.Play("Fire");
        AddProjectile();

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
            isRPGReloading = true;
            reloadSpeed = 1.5f;

            yield return new WaitForSeconds(reloadSpeed);
            isRPGReloading = false;
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
