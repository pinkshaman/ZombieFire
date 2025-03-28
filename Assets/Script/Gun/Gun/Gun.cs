﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        var originalGun = GunManager.Instance.GetGun(GunName);
        var newGun = CloneGunData(originalGun);
        
        gunPlayer = GunManager.Instance.ReturnPlayerGun(newGun.GunName);
        
        Initialize(newGun,gunPlayer);
        itemUI = FindFirstObjectByType<GamePlayUI>();
        OnReloading.AddListener(itemUI.UserReloadItem);
    }
    public virtual void Aiming()
    {
        OnAiming.Invoke();
    }

    public virtual void Initialize(BaseGun gunData, PlayerGun playerGun)
    {
        this.gunData = gunData;
        this.gunPlayer = playerGun;
        ApplyUpgradeEffects(gunData);
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
        float newSpeed = 3.0f;
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
    public BaseGun CloneGunData(BaseGun originalGun)
    {
        BaseGun newGun = new BaseGun
        {
            GunName = originalGun.GunName,
            gunStats = new GunStats
            {
                ammoCapacity = originalGun.gunStats.ammoCapacity,
                fireRate = originalGun.gunStats.fireRate,
                damage = originalGun.gunStats.damage,
                critical = originalGun.gunStats.critical
            },
            gunModel = new GunModel
            {
                gunType = originalGun.gunModel.gunType,
                gunModel = originalGun.gunModel.gunModel,
                gunSprite = originalGun.gunModel.gunSprite,
                gunDecription = originalGun.gunModel.gunDecription,
                reloadAnimationClip = originalGun.gunModel.reloadAnimationClip
            },
            upgradeList = new GunUpgradeList()
            {
                gunUgradeList = new List<GunUpgrade>()
            },
            buyGun = originalGun.buyGun
        };
        foreach (var upgrade in originalGun.upgradeList.gunUgradeList)
        {
            newGun.upgradeList.gunUgradeList.Add(new GunUpgrade
            {
                starUpgrade = upgrade.starUpgrade,
                powerUpgrade = upgrade.powerUpgrade,
                criticalUpgrade = upgrade.criticalUpgrade,
                fireRateUpgrade = upgrade.fireRateUpgrade
            });
        }
        return newGun;
    }

    public void ApplyUpgradeEffects(BaseGun gun)
    {
        for (int i = 1; i <= gunPlayer.starUpgrade; i++)
        {
            var baseUpgrade = gun.upgradeList.gunUgradeList.Find(upgrade => upgrade.starUpgrade == i);
            if (baseUpgrade != null)
            {
                gun.gunStats.damage += baseUpgrade.powerUpgrade;
                gun.gunStats.critical += baseUpgrade.criticalUpgrade;
                gun.gunStats.fireRate += baseUpgrade.fireRateUpgrade;
            }
        }
    }

    private void CancelReload()
    {
        isReloading = false;
        anim.speed = 1f;
        Debug.Log("FastReload Canceled , animator Speed reset to default");
    }
    public bool IsValidFireInput()
    {
        if (IsTouchingUI()) return false;

        if (Input.GetMouseButton(0) && IsInFireZone(Input.mousePosition))
        {
            return true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && IsInFireZone(touch.position))
            {
                return true;
            }
        }

        return false;
    }
    private bool IsTouchingUI()
    {
        if (Input.touchCount > 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("Enemy"))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    private bool IsInFireZone(Vector2 position)
    {
        return position.x > Screen.width / 2;
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
        if(Input.GetKeyDown(KeyCode.C))
        {
            itemUI.UserShieldItem();
        }
       
    }
}
