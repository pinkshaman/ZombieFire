using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAlert : WarningAlert
{
    public Text startWave;

    public void StartAlerts()
    {
        startWave.text = $"WAVE {1}";
        AlertPlay();
    }
    public override void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }


}
