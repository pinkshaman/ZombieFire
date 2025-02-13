using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadShotAlert : WarningAlert
{
    public Text headShotCount;
    public override void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }
    public void HeadShotAlertCount(float number)
    {
        if (number > 0)
        {
            headShotCount.text = $"{number}";
        }
        
        anim.Play();
    }

}
