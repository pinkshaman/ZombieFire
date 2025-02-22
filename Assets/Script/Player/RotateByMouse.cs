using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateByMouse : MonoBehaviour
{
    public float sensitivity = 0.1f; // Độ nhạy xoay camera
    public float minPitch = -30f;
    public float maxPitch = 60f;
    public float minYaw = -90f;
    public float maxYaw = 90f;

    private float pitch;
    private float yaw;
    private Vector2 lastInputPosition;
    private bool isDragging = false;

    public Transform horizontalPivot; // Quay ngang (yaw)
    public Transform verticalPivot; // Quay dọc (pitch)

    private void Start()
    {
        yaw = horizontalPivot.localEulerAngles.y;
        pitch = verticalPivot.localEulerAngles.x;
    }

    private void Update()
    {
        HandleMouseRotation();
        HandleTouchRotation();
    }

    private bool IsInRotationZone(Vector2 position)
    {
        if (IsTouchingNonEnemyUI()) return false;
        return position.x < Screen.width / 2;
    }

    private void HandleMouseRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsInRotationZone(Input.mousePosition))
            {
                lastInputPosition = Input.mousePosition;
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastInputPosition;
            lastInputPosition = Input.mousePosition;

            RotateCamera(delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!IsInRotationZone(touch.position))
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastInputPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 delta = touch.position - lastInputPosition;
                        lastInputPosition = touch.position;
                        RotateCamera(delta);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
    private bool IsTouchingNonEnemyUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Enemy"))
                continue; // Bỏ qua UI có tag "Enemy"

            return true; // Nếu có UI khác "Enemy", không cho phép xoay
        }

        return false; // Không có UI nào cản trở, cho phép xoay
    }

    private void RotateCamera(Vector2 delta)
    {
        // Cập nhật góc yaw (xoay ngang)
        yaw += delta.x * sensitivity;
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        horizontalPivot.localRotation = Quaternion.Euler(0, yaw, 0);

        // Cập nhật góc pitch (xoay dọc)
        pitch -= delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        verticalPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

}
