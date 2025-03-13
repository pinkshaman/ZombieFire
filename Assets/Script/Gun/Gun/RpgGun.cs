using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgGun : Gun
{
    public RPG rpgGun;
    private float reloadSpeed;
    public bool isRPGReloading = false;

    public override void Update()
    {
        if (IsValidFireInput() || Input.GetKey(KeyCode.V) && !isRPGReloading)
        {
            Shooting();
        }
    }
    public void DeavtiveRocket()
    {
        rpgGun.rocket.SetActive(false);
    }

    public IEnumerator ShootBullet()
    {
        gunAmmo.SingleFireAmmoCounter();
        anim.SetTrigger("Fire");
        Fire();
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

    void Fire()
    {
        GameObject rocket = RocketPooling.Instance.GetRocket();
        rocket.GetComponent<Rocket>().ShootRocket(transform.position, transform.rotation);
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
