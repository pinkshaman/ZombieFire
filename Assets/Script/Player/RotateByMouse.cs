using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMouse : MonoBehaviour
{
    [Header("Settings")]
    public float angleOverDistance;
    public Transform horizontalPivot;
    public Transform verticalPivot;
    public float minPitch;
    public float maxPitch;
    public float minYaw ;
    public float maxYaw ;
    private float pitch; 
    private float yaw;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateYaw();
        UpdatePitch(); 
    }

    private void UpdateYaw()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float deltaYaw = mouseX * angleOverDistance;
        yaw = Mathf.Clamp(yaw+ deltaYaw, minYaw, maxYaw);
        horizontalPivot.localEulerAngles = new Vector3(0, yaw, 0);
    }

    private void UpdatePitch()
    { 
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaPitch = -mouseY * angleOverDistance; 
        pitch = Mathf.Clamp(pitch + deltaPitch, minPitch, maxPitch);
        verticalPivot.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    public void ResetCameraRotation(Transform newGun)
    {
        // Lấy góc hiện tại của camera
        newGun.rotation = verticalPivot.rotation;
    }

}
