using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SurvivalModeProgess
{
    public int highestScore;
    public int highestWave;
}

public class SurvivalMode : MonoBehaviour
{
    public static SurvivalMode Instance { get; private set; }
    public SurvivalStage survivalStage;
    public SurvivalModeProgess survivalModeProgess;
    public int survivalCount;
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
        LoadSuvivalMode();
        LoadSurvivalCount();
    }
    public void UpdateRequireTimes()
    {
        if (survivalCount >= 10) return;
        survivalCount++;
        SaveSurvivalCount();
    }
    public void UpdateRank(SurvivalModeProgess progess)
    {
        if (survivalModeProgess.highestScore < progess.highestScore)
        {
            survivalModeProgess.highestScore = progess.highestScore;
        }
        if (survivalModeProgess.highestWave < progess.highestWave)
        {
            survivalModeProgess.highestWave = progess.highestWave;
        }
    }


    [ContextMenu("SaveSuvivalMode")]
    public void SaveSuvivalMode()
    {
        var value = JsonUtility.ToJson(survivalModeProgess);
        PlayerPrefs.SetString(nameof(survivalModeProgess), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadSuvivalMode")]
    public void LoadSuvivalMode()
    {
        var defaultValue = JsonUtility.ToJson(survivalModeProgess);
        var json = PlayerPrefs.GetString(nameof(survivalModeProgess), defaultValue);
        survivalModeProgess = JsonUtility.FromJson<SurvivalModeProgess>(json);
        Debug.Log("Highest Score is Loaded");
    }
    public void SaveSurvivalCount()
    {
        PlayerPrefs.SetString("survivalCount", survivalCount.ToString());
        PlayerPrefs.Save();
    }
    public void LoadSurvivalCount()
    {
        if (PlayerPrefs.HasKey("survivalCount"))
        {
            survivalCount = int.Parse(PlayerPrefs.GetString("survivalCount"));
        }
        else
        {
            survivalCount = 0;
            SaveSurvivalCount();
        }
    }

}
