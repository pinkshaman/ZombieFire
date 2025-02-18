using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOption : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider controlSlider;

    public Button mainMenuButton;
    public Button returnToGameButton;
    private OptionData optionData;
    public void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mainMenuButton.onClick.AddListener(MainMenuButton);
        returnToGameButton.onClick.AddListener(ReturnToGameButton);
        SetData();
        volumeSlider.onValueChanged.AddListener(FillVolume);
        controlSlider.onValueChanged.AddListener(FillControl);
    }
    public void SetData()
    {
        var data = AudioManager.Instance.ReturnOptionData();
        this.optionData = data;
        volumeSlider.value = optionData.vomlume;
        controlSlider.value = optionData.control;
       
    }
    public void FillVolume(float volumePercent)
    {
        AudioListener.volume = volumePercent;
        optionData.vomlume = volumePercent;
        
        AudioManager.Instance.UpadateData(optionData);
    }
    public void FillControl(float value)
    {

    }
    public void MainMenuButton()
    {
        MySceneManager.Instance.LoadSceneByID(0);
    }
    public void ReturnToGameButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        this.gameObject.SetActive(false);
    }
}
