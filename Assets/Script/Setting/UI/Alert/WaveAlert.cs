using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlert : WarningAlert
{
    public Text waveNumber;
    public void WaveWarning(float number)
    {
        waveNumber.text = $"WAVE {number}";
        AlertPlay();
    }

    public override void PlaySoundAlert()
    {
        audioSource.clip = alertData.audio;
        audioSource.Play();
    }
}
