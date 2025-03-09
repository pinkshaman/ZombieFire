using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGameOption : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider controlSlider;
    public Slider BMGSlider;
    public AudioSource BGMSource;
    private OptionData optionData;
    public Text playerID;
    public Text versionText;

    public Button showLeaderBoad;
    public void Start()
    {
        SetData();
        volumeSlider.onValueChanged.AddListener(FillVolume);
        controlSlider.onValueChanged.AddListener(FillControl);
        BMGSlider.onValueChanged.AddListener(FillBMG);
    }
    public void SetData()
    {
        var data = AudioManager.Instance.ReturnOptionData();
        this.optionData = data;
        volumeSlider.value = optionData.vomlume;
        controlSlider.value = optionData.control;
        BMGSlider.value = optionData.bmg;
    }
    public void FillVolume(float volumePercent)
    {
        AudioListener.volume = volumePercent;
        optionData.vomlume = volumePercent;

        AudioManager.Instance.UpadateData(optionData);
    }
    public void FillControl(float value)
    {
        optionData.control = value;
        AudioManager.Instance.UpadateData(optionData);
    }
    public void FillBMG(float value)
    {
        optionData.bmg = value;
        AudioManager.Instance.UpadateData(optionData);
        BGMSource.volume = value;
    }
    public void SetID()
    {
        var ID = PlayGamesPlatform.Instance.GetUserId();
        playerID.text = $"User ID :{ID}";
        var version = Application.version;
        versionText.text = $"V.{version}";
    }
    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_survival_leaderboard);
    }
    
  
}
