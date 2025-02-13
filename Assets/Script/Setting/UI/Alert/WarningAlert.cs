using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningAlert : MonoBehaviour
{
    public AlertType alertType;
    public AlertData alertData;
    public AudioSource audioSource;
    public Animation anim;

    public virtual void Start()
    {
        alertData = AlertManager.Instance.InitAlertData(alertType);
        Debug.Log($"AlertData: {alertData.alertType}");
    }
    public virtual void AlertPlay()
    {
        anim.Play();
    }
    public virtual void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }
 

}
