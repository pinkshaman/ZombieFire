using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DrawRay : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GunRayCaster rayCaster;
    public Transform firingPos;
  
    public void DrawRaycast(RaycastHit hit)
    {
        StartCoroutine(DrawRayWithDelay(hit));
    }
    private IEnumerator DrawRayWithDelay(RaycastHit hit)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, firingPos.position);
        lineRenderer.SetPosition(1, hit.point);
        lineRenderer.startWidth = 0.009f;
        lineRenderer.endWidth = 0.001f;
        lineRenderer.endColor = Color.yellow;      
        yield return new WaitForSeconds(0.15f);      
        lineRenderer.positionCount = 0;      
    }
    public void Start()
    {
        rayCaster = FindObjectOfType<GunRayCaster>();
        if (rayCaster == null) return;
        rayCaster.onRaycasting.AddListener(DrawRaycast);

    }


}
