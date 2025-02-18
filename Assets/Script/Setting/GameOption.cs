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

    public void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mainMenuButton.onClick.AddListener(MainMenuButton);
        returnToGameButton.onClick.AddListener(ReturnToGameButton);
    }

    public void FillVolume()
    {
        float volumePercent = volumeSlider.value;

        AudioListener.volume = volumePercent;

    }
    public void FillControl()
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
