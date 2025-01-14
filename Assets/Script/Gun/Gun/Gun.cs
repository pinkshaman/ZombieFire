using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Gun : MonoBehaviour
{
    public string GunName;
    public BaseGun gunData;
    public AudioSource audioSource;
    public Transform aimingPos;
    public Animator anim;
    public GunAmmo gunAmmo;
    public bool haveScope;

    public UnityEvent OnSwitching;
    public UnityEvent OnShooting;
    public UnityEvent OnReloading;
    public UnityEvent OnAiming;

    public virtual void Start()
    {
        if (GunManager.Instance == null)
        {
            Debug.LogError("GunManager instance is missing.");
            return;
        }
        var newGun = GunManager.Instance.GetGun(GunName);
        Initialize(newGun);
    }
    public virtual void Aiming()
    {
        OnAiming.Invoke();
    }

    public virtual void Initialize(BaseGun gunData)
    {
        this.gunData = gunData;

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

    public abstract void ReLoading();

    public virtual void Hiding()
    {
        anim.Play("Hide");
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
        //anim.ResetTrigger("Reload");
        //anim.ResetTrigger("Fire");
        anim.SetTrigger("Idle");
    }

}
