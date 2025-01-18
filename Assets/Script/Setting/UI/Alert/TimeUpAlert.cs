using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpAlert : WarningAlert
{
    public override void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }

}
