using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningAlert : MonoBehaviour
{
    public Text waveNumber;
    public Animation warningAnimation;
    public AudioSource warningAudioSource;

    public void Start()
    {
        
    }
    public void WarningPlay(float number)
    {
        waveNumber.text =$"WAVE {number}";
        warningAnimation.Play();
        warningAudioSource.Play();      
    }


}
