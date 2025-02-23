using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public OptionData optionData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        LoadOptionData();
        SetVolume(optionData.vomlume);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;   
    }
    public void UpadateData(OptionData newData)
    {
        optionData = newData;
        SaveOptionData();
    }
   public OptionData ReturnOptionData()
    {
        return optionData;
    }
    [ContextMenu("SaveOptionData")]
    public void SaveOptionData()
    {
        var value = JsonUtility.ToJson(optionData);
        PlayerPrefs.SetString(nameof(optionData), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadOptionData")]
    public void LoadOptionData()
    {
        var defaultValue = JsonUtility.ToJson(optionData);
        var json = PlayerPrefs.GetString(nameof(optionData), defaultValue);
        optionData = JsonUtility.FromJson<OptionData>(json);
        Debug.Log("ArenVsStage are Loaded");
    }


}
