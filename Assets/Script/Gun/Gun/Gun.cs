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
    public GamePlayUI itemUI;
    public bool haveScope;
    private bool isReloading;

    public UnityEvent OnSwitching;
    public UnityEvent OnShooting;
    public UnityEvent OnReloading;
    public UnityEvent OnAiming;

    public virtual void Start()
    {     
        var newGun = GunManager.Instance.GetGun(GunName);
        gunPlayer = GunManager.Instance.ReturnPlayerGun(newGun.GunName);
        Initialize(newGun);
        itemUI= FindObjectOfType<GamePlayUI>();
        OnReloading.AddListener(itemUI.UserReloadItem);
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
        bool isAvailable = itemUI.fastRealoadCount > 0;
        Debug.Log($"FastReloadItem : {isAvailable}");
        if (!isAvailable)
        {
            anim.SetTrigger("Reload");
            anim.speed = 1;
        }
        else
        {
            FastReloadItem();
        }
        isReloading = true;
    }
    public virtual void FastReloadItem()
    {
        float newSpeed = 2.0f;  
        anim.SetTrigger("Reload");
        itemUI.UserReloadItem();
        anim.speed = newSpeed;
        isReloading = true;
        StartCoroutine(ResetController());
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
    IEnumerator ResetController()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            yield return null;
        }
        anim.speed = 1f;
        Debug.Log($"ResetReloadTime: {anim.speed}");
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
    private void CancelReload()
    {
        isReloading = false;
        anim.speed = 1f;
        Debug.Log("Reload Canceled , animator Speed reset to default");
    }
    public virtual void Update()
    {
        if (isReloading)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Reload"))
            {
                CancelReload();
            }
        }
    }
}
