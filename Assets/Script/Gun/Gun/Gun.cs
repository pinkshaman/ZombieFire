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
    public Animator anim;
    public AudioSource audioSource;
    public GunAmmo gunAmmo;
    public Transform aimingPos;
    public bool haveScope;
    

    public UnityEvent onShoot;
    public UnityEvent onAiming;
    public UnityEvent onReLoad;


    public virtual void Start()
    {
        gunData = GunManager.Instance.GetGun(gunName);
        Debug.Log($"Loaded GunData:{gunData.GunName} ");
        anim = GetComponent<Animator>();
        anim.SetTrigger("Ready");
        gunAmmo.loadedAmmoChanged.AddListener(OutOfAmmo);
    }
    public void ShootState()
    {
        PlayState("Fire", gunData.ShootingSound);
    }
    public void ReloadState()
    {      
        PlayState("Reload", gunData.ReloadSound);
        onReLoad.Invoke();
    }
    public void ReadyState()
    {
        PlayState("Ready", gunData.ReadySound);

    }
    public void OutOfAmmo()
    {
        if (gunAmmo.LoadedAmmo <= 0)
        {
            PlayState(null, gunData.OutofAmmoSound);
        }
    }

    public void PlayState(string animationState, AudioClip clip)
    {
        anim.Play(animationState);
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void Aim()
    {
        onAiming?.Invoke();
    }
    public float ReturnReloadTimes()
    {
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if(clip.name == "Reload" )
            {
                return (clip.length );
            }
           
        }   
        return 0;
    }
}
