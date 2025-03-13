using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateByMouse : MonoBehaviour
{
    public float sensitivity;
    public float minPitch;
    public float maxPitch;
    public float minYaw;
    public float maxYaw;

    private float pitch;
    private float yaw;
    private Vector2 lastInputPosition;
    private bool isDragging = false;
    private int activeFingerId = -1;


    public Transform horizontalPivot;
    public Transform verticalPivot;
    public GameOption gameOption;
    private void Start()
    {
        gameOption.controlSlider.onValueChanged.AddListener(SetDataSensitivity);
        SetDataSensitivity(gameOption.controlSlider.value);
        yaw = horizontalPivot.localEulerAngles.y;
        pitch = verticalPivot.localEulerAngles.x;
        
    }
    public void SetDataSensitivity(float ammout)
    {
        sensitivity = ammout;
    }
    private void Update()
    {
        HandleMouseRotation();
        HandleTouchRotation();
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private bool IsInRotationZone(Vector2 position)
    {
        if (IsTouchingNonEnemyUI()) return false;
        return position.x < Screen.width / 2;
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            RotateCamera(new Vector2(mouseX, mouseY) * 10f);
        }
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount == 0)
        {
            activeFingerId = -1;
            isDragging = false;
            return;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (!IsInRotationZone(touch.position))
                continue;

            if (activeFingerId == -1 || activeFingerId == touch.fingerId)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        activeFingerId = touch.fingerId;
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
                    case TouchPhase.Canceled:
                        if (activeFingerId == touch.fingerId)
                        {
                            isDragging = false;
                            activeFingerId = -1;
                        }
                        break;
                }
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
                continue;
            return true;
        }

        return false;
    }


    private void RotateCamera(Vector2 delta)
    {
        float deltaYaw = delta.x * sensitivity;
        yaw = Mathf.Clamp(yaw + deltaYaw, minYaw, maxYaw);
        horizontalPivot.localEulerAngles = new Vector3(0, yaw, 0);

        float deltaPitch = -delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch + deltaPitch, minPitch, maxPitch);
        verticalPivot.localEulerAngles = new Vector3(pitch, 0, 0);
    }
}
