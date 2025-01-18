using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Health health;
    public Image HPbar;
    public Image FillBar;
    public Text healthAmounText;

    private Transform cameraTransform;

    private void Start()
    {

        cameraTransform = FindObjectOfType<Camera>().transform;
    }
    public void Fill(int currentHealth, int totalHealth)
    {
        var fillPercent = 1f * currentHealth / totalHealth;
        FillBar.fillAmount = fillPercent;    
    }
    public void FacingPlayer()
    {
        if (cameraTransform == null)
        {
            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                cameraTransform = mainCamera.transform;
            }
            else
            {
                Debug.LogError("Không tìm thấy Camera trong scene.");
                return;
            }
        }
        transform.forward = -cameraTransform.forward;
    }
    public void ShowText()
    {
        healthAmounText.text = $"HP : {health.maxHealthPoint}";
    }


}
