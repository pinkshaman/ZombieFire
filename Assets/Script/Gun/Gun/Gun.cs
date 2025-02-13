using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Gun : MonoBehaviour
{
    public string GunName;
    public BaseGun gunData;
    public PlayerGun gunPlayer;
    public AudioSource audioSource;
    public Transform aimingPos;
    public Animator anim;
    public GunAmmo gunAmmo;
    public bool haveScope;
    private float originalDuration;

    public UnityEvent OnSwitching;
    public UnityEvent OnShooting;
    public UnityEvent OnReloading;
    public UnityEvent OnAiming;

    public virtual void Start()
    {
        
        var newGun = GunManager.Instance.GetGun(GunName);
        gunPlayer = GunManager.Instance.ReturnPlayerGun(newGun.GunName);
        Initialize(newGun);
        originalDuration = ReturnReloadTime();
    }
    public virtual void Aiming()
    {
        OnAiming.Invoke();
    }

    public virtual void Initialize(BaseGun gunData)
    {
        this.gunData = gunData;
        UpgradeGearEffect(this.gunData);
        Debug.Log($"SetData :{gunData.GunName}");
    }
    public virtual void Switching()
    {
        ResetAnimation();
        OnSwitching.Invoke();
    }

    public virtual void RemoveAllLisstenner()
    {

    }
    public virtual void AddAllLisstenner()
    {

    }
    public abstract void Shooting();

    public virtual void ReLoading()
    {
        //bool isAvailable = PlayerManager.Instance.ReturnItemReloadInfor();
        //Debug.Log($"FastReloadItem : {isAvailable}");
        //if (!isAvailable)
        //{
            //float time = originalDuration;
            anim.SetTrigger("Reload");
            //anim.speed = time; 
        //}
        //else
        //{
        //    FastReloadItem();
        //}
    }
    public virtual void FastReloadItem()
    {
        float newDuration = originalDuration * (1.0f / 1.5f);
        float newSpeed = originalDuration / newDuration;
        anim.SetTrigger("Reload");

        anim.speed = newSpeed;
        StartCoroutine(ResetController(newSpeed));
    }
    public virtual void Hiding()
    {
        anim.SetTrigger("Hide");
    }

    public virtual void Ready()
    {
        anim.SetTrigger("Ready");
    }
    public virtual void Idle()
    {
        anim.SetTrigger("Idle");
    }
    public virtual void ResetAnimation()
    {
        anim.ResetTrigger("Reload");
        anim.ResetTrigger("Fire");
        anim.SetTrigger("Idle");
    }
    public virtual float ReturnReloadTime()
    {
        foreach (var clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Reload")
            {
                float reloadTime = clip.length;
                Debug.Log($"ReloadTime : {reloadTime}-{GunName}");
                return reloadTime;
            }
        }
        return 0.0f;
    }
    public virtual float ReturnHideTime()
    {
        foreach (var clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Hide")
            {
                float hideTime = clip.length;
                Debug.Log($"ReloadTime : {hideTime}-{GunName}");
                return hideTime;
            }
        }
        return 0.0f;
    }
    IEnumerator ResetController(float duration)
    {

        yield return new WaitForSeconds(duration);
        anim.speed = 1;
        Debug.Log($"ResetReloadTime: {anim.speed} ");

    }
    public void UpgradeGearEffect(BaseGun gun)
    {
        for (int i = 0; i < gunPlayer.starUpgrade; i++)
        {
            var baseUpgrade = gun.upgradeList.gunUgradeList.Find(baseUpgrade => baseUpgrade.starUpgrade == gunPlayer.starUpgrade);
            gun.gunStats.damage += baseUpgrade.powerUpgrade;
            gun.gunStats.critical += baseUpgrade.criticalUpgrade;
            gun.gunStats.fireRate += baseUpgrade.fireRateUpgrade;
        }
    }
}
