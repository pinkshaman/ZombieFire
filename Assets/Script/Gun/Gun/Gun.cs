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

    public UnityEvent OnSwitching;
    public UnityEvent OnShooting;
    public UnityEvent OnReloading;

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

    public virtual void Initialize(BaseGun gunData)
    {
        this.gunData = gunData;
        Debug.Log($"SetData :{gunData.GunName}");
    }
    public virtual void Switching()
    {
       
        var gun = GunManager.Instance?.FindActiveGun().gunData;
        Initialize(gun);
        OnSwitching?.Invoke();
    }
    public virtual void RemoveAllLisstenner()
    {

    }
    public virtual void AddAllLisstenner()
    {

    }
    public abstract void Shooting();

    public abstract void ReLoading();

    public abstract void Hiding();
  
    public abstract void Ready();
  
}
