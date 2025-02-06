using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUi : MonoBehaviour
{
    public Button button;
    public AlertType alertType;
    protected AlertData alertData;
    public AudioSource audioSource;
    public virtual void Start()
    {
        alertData = AlertManager.Instance.InitAlertData(alertType);
        LoadButton();
        AddonClickEvent();
    }
    public virtual void LoadButton()
    {
        if (button != null) return;
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogWarning(transform.name + "does not have a Button Component", gameObject);
        }
        else
        {
            Debug.LogWarning(transform.name + "LoadButton", gameObject);
        }
    }
    public virtual void AddonClickEvent()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogWarning("Button coponent is not assigned or not found ", gameObject);
        }
    }
    public virtual void OnClick()
    {
        Debug.Log("Button Clicked!");
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }
}
