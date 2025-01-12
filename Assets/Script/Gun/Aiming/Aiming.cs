using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera mainCamera;
    public Camera aimingCamera;

    [Header("Camera Position Settings")]
    public Gun gun;
    public GameObject scope;

    public void Awake()
    {
        gun = FindObjectOfType<Gun>();
    }
    private void Start()
    {
        aimingCamera.enabled = false;
        gun.OnAiming.AddListener(AimCamera);
        gun.OnReloading.AddListener(ResetAimingCamera);
        gun.OnSwitching.AddListener(ResetAimingCamera);
    }
  
    public void InitNewGun()
    {
        var newGun = GunManager.Instance.FindActiveGun();
        gun.aimingPos = newGun.aimingPos;
        Debug.Log("init New AimingPos");
    }
    public void AimCamera()
    {

        if (aimingCamera.enabled)
        {
            ResetAimingCamera();

        }
        else if (!aimingCamera.enabled)
        {
            ActivateAimingCamera();
        }
    }
    private void ActivateAimingCamera()
    {
        aimingCamera.enabled = true;
        mainCamera.enabled = false;
        aimingCamera.transform.position = gun.aimingPos.position;
        ActiveScope();
    }
    public void ActiveScope()
    {
        if (gun.haveScope)
        {
            scope.SetActive(true);
            int playerLayer = LayerMask.NameToLayer("Weapon");
            aimingCamera.cullingMask &= ~(1 << playerLayer);
        }
    }
    public void DisableScope()
    {
        if (gun.haveScope)
        {
            scope.SetActive(false);
            int playerLayer = LayerMask.NameToLayer("Weapon");
            aimingCamera.cullingMask |= (1 << playerLayer);
        }
    }
    private void ResetAimingCamera()
    {
        aimingCamera.enabled = false;
        mainCamera.enabled = true;
        DisableScope();
        InitNewGun();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AimCamera();
        }

    }
}
