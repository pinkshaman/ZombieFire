using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunModelInventory : MonoBehaviour
{
    public string gunName;
    public GameObject model;
    public float rotationSpeed = 20f;
    private bool isHolding = false;
    private Vector3 lastMousePosition;
    public void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    public void Hold()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime);
        lastMousePosition = Input.mousePosition;
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    public void Update()
    {
        if (!isHolding)
        {
            Rotate();
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            isHolding = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
        }
        if (isHolding)
        {
            Hold();
        }

    }
}
