using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControlUi : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider SFXSoundSlider;
    public Toggle muteAll;
    public AudioManager audioManager;
    public AudioSource musicAudioSource;
    public AudioSource SFXAudioSource;

    private float previousMusicVolume;
    private float previousSFXVolume;
    //public void Start()
    //{
    //    audioManager = FindAnyObjectByType<AudioManager>();
    //    muteAll.onValueChanged.AddListener(OnToggleValueChanged);
    //    musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    //    SFXSoundSlider.onValueChanged.AddListener(SetSFXVolume);

    //    musicVolumeSlider.value = musicAudioSource.volume;
    //    SFXSoundSlider.value = SFXAudioSource.volume;

    //    previousMusicVolume = musicVolumeSlider.value;
    //    previousSFXVolume = SFXSoundSlider.value;

    //}

    //public void OnToggleValueChanged(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        previousMusicVolume = audioManager.backGroundMusic.volume;
    //        previousSFXVolume = audioManager.soundEffect.volume;
    //        audioManager.backGroundMusic.volume = 0f;
    //        audioManager.soundEffect.volume = 0f;
    //    }
    //    else
    //    {
    //        audioManager.backGroundMusic.volume = previousMusicVolume;
    //        audioManager.soundEffect.volume = previousSFXVolume;
    //    }
    //}
    //public void SetMusicVolume(float volume)
    //{
    //    audioManager.backGroundMusic.volume = volume;
    //}
    //public void SetSFXVolume(float volume)
    //{
    //    audioManager.soundEffect.volume = volume;
    //}
}
