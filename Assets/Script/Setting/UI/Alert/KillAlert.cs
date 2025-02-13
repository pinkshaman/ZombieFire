using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KillAlert : WarningAlert
{
    public override void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }
    public  void KillAlertPlay()
    {      
        anim.Play();
    }

}
