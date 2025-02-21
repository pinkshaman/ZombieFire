using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Health health;
    public Image HPbar;
    public Image FillBar;
    public AutoFade autoFade;
    private Transform cameraTransform;

    public virtual void Start()
    {
        cameraTransform = FindObjectOfType<Camera>().transform;
    }
    public virtual void Fill(int currentHealth, int totalHealth)
    {
        autoFade.Show();
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
   
}
