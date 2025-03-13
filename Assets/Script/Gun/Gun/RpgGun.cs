using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgGun : Gun
{
    public RPG rpgGun;
    public float bulletSpeed;

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
    public void ActiveRocket()
    {
        rpgGun.rocket.SetActive(true);
    }

    public IEnumerator ShootBullet()
    {
        gunAmmo.SingleFireAmmoCounter();
        anim.Play("Fire");
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
            ActiveRocket();
            isRPGReloading = true;
            reloadSpeed = 1.5f;
            anim.SetTrigger("Reload");
            yield return new WaitForSeconds(reloadSpeed);
            isRPGReloading = false;
            gunAmmo.UnlockShooting();
        }
    }

   public void Fire()
    {
        DeavtiveRocket();

        GameObject rocket = RocketPooling.Instance.GetRocket();
        rocket.transform.SetPositionAndRotation(rpgGun.firingPos.position,rpgGun.firingPos.rotation);
        rocket.GetComponent<Rigidbody>().velocity = rpgGun.firingPos.forward * bulletSpeed;
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
