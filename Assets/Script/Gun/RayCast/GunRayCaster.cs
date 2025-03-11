using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GunRayCaster : MonoBehaviour
{
    public Camera aimingCamera;
    public LayerMask layerMask;
    public HitEffectManager hitEffectManager;
    public Gun gun;
    public UnityEvent<RaycastHit> onRaycasting;
    public DamageManagement damageManagement;
    public Recoil recoil;

    public void Start()
    {
        hitEffectManager = FindObjectOfType<HitEffectManager>();
        damageManagement = FindObjectOfType<DamageManagement>();
    }
    public void PerformRayCasting()
    {
        //Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        Ray aimingRay = recoil.CalculateRecoilRay();
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000, layerMask))
        {
            Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            HitEffect(hitInfo, effectRotation);
        }
        recoil.ApplyRecoil();
        onRaycasting.Invoke(hitInfo);
    }
    public void HitEffect(RaycastHit hitInfo, Quaternion effectRotation)
    {
        var hitSurface = hitInfo.collider.GetComponent<HitSurface>();
        if (hitSurface != null)
        {
            var effectPrefabs = HitEffectManager.Instance.GetEffectPrefabs(hitSurface.hitSurFaceType);
            Instantiate(effectPrefabs, hitInfo.point, effectRotation);

            Helmet helmet = hitInfo.collider.GetComponent<Helmet>();
            if (helmet != null)
            {
                helmet.TakeHit();
            }
            Shield shield = hitInfo.collider.GetComponent<Shield>();
            if (shield != null)
            {
                shield.TakeHit();
                return;
            }
            
            DeliverDamage(hitInfo);
        }
        Debug.Log($"{hitInfo.collider.gameObject.tag}");
    }
    public void DeliverDamage(RaycastHit hitInfo)
    {
        ZombieHealth health = hitInfo.collider.GetComponentInParent<ZombieHealth>();
        if (health != null)
        {
            health.CheckHeadShot(hitInfo);
            damageManagement.Calculator(hitInfo, gun.gunData.gunStats.damage, health, gun.gunData.gunStats.critical);
            health.TakeDamage(gun.gunData.gunStats.damage);
        }
    }


}
