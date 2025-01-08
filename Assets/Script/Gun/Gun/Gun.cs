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
        gunData.Anim = GetComponent<Animator>();
        gunData.Anim.SetTrigger("Ready");
    }
    public void ShootState()
    {      
        PlayState("Fire", gunData.ShootingSound);      
    }
    public void ReloadState()
    {
        onReLoad.Invoke();
        PlayState("Reload", gunData.ReloadSound);
        
    }
    public void ReadyState()
    {
        PlayState("Ready", gunData.ReadySound);
    }
    public void OutOfAmmo()
    {
        PlayState(null, gunData.OutofAmmoSound);
    }
    public void PlayState(string animationState , AudioClip clip)
    {
        gunData.Anim.Play(animationState);
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void Aim()
    {
        onAiming?.Invoke();
    }
    public void ShellBullet()
    {
        onShoot.AddListener(shellBullet.Play);
    }
  
}
