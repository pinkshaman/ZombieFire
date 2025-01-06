using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public BaseGun gunData;
    private Animation anim;
    public AudioSource audioSource;

    public virtual void Start()
    {
        gunData = GunManager.Instance.GetGun(gunName);
        anim = GetComponent<Animation>();
    }
    public void ShootState()
    {
        anim.clip = gunData.Fire;
        anim.Play();
        audioSource.clip = gunData.ShootingSound;
        audioSource.Play();
    }
    public void ReloadState()
    {
        anim.clip = gunData.Reload;
        anim.Play();
        audioSource.clip= gunData.ReloadSound;
        audioSource.Play();
    }
    public void ReadyState()
    {
        anim.clip = gunData.Ready;
        anim.Play();
        audioSource.clip = gunData.ReadySound;
        audioSource.Play();
    }
  
}
