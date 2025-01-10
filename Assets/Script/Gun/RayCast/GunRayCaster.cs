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


    public void PerformRayCasting()
    {
        Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000, layerMask))
        {
            Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            HitEffect(hitInfo, effectRotation);
        }
        onRaycasting.Invoke(hitInfo);
    }
    public void HitEffect(RaycastHit hitInfo, Quaternion effectRotation)
    {
        var hitSurface = hitInfo.collider.GetComponent<HitSurface>();
        if (hitSurface != null)
        {
            var effectPrefabs = HitEffectManager.Instance.GetEffectPrefabs(hitSurface.hitSurFaceType);
            Instantiate(effectPrefabs, hitInfo.point, effectRotation);
            DeliverDamage(hitInfo);

        }
        Debug.Log($"{hitInfo.collider.gameObject.tag}");
    }
    public void DeliverDamage(RaycastHit hitInfo)
    {
        Health health = hitInfo.collider.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(gun.gunData.gunStats.damage);
        }
    }
}
