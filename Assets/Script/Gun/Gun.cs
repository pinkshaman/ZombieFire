using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public BaseGun gunData;
    [Header("General Properties")]
    public string gunName;
    private Animation anim;
    public AudioSource audioSource;
    public GunAmmo gunAmmo;
    public Transform aimingPos;
    public ParticleSystem shellBullet;
    public bool haveScope;
   
    public UnityEvent onShoot;
    public UnityEvent onAiming;
    public UnityEvent onReLoad;
    public  virtual void Start()
    {
        gunData = GunManager.Instance.GetGun(gunName);      
        anim = GetComponent<Animation>();
    }
    public void ShootState()
    {      
        PlayState(gunData.Fire, gunData.ShootingSound);
        
    }
    public void ReloadState()
    {
        onReLoad.Invoke();
        PlayState(gunData.Reload, gunData.ReloadSound);
        
    }
    public void ReadyState()
    {
        PlayState(gunData.Ready, gunData.ReadySound);
    }
    public void OutOfAmmo()
    {
        PlayState(null, gunData.OutofAmmoSound);
    }
    public void PlayState(AnimationClip clip ,AudioClip sound)
    {
        anim.clip = clip;
        anim.Play();
        audioSource.clip = sound;
        audioSource.Play();
    }
    public void Aim()
    {
        onAiming?.Invoke();
    }
    public void ShellBullet()
    {
        onReLoad.AddListener(shellBullet.Play);
    }
  
}
