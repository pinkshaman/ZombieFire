using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SuvivalModeProgess
{
    public int highestScore;
    public int highestWave;
}

public class SurvivalMode : MonoBehaviour
{
    public static SurvivalMode Instance { get; private set; }
    public SurvivalStage survivalStage;
    public SuvivalModeProgess suvivalModeProgess;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Start()
    {
        LoadSuvivalMode();
    }



    [ContextMenu("SaveSuvivalMode")]
    public void SaveSuvivalMode()
    {
        var value = JsonUtility.ToJson(suvivalModeProgess);
        PlayerPrefs.SetString(nameof(suvivalModeProgess), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadSuvivalMode")]
    public void LoadSuvivalMode()
    {
        var defaultValue = JsonUtility.ToJson(suvivalModeProgess);
        var json = PlayerPrefs.GetString(nameof(suvivalModeProgess), defaultValue);
        suvivalModeProgess = JsonUtility.FromJson<SuvivalModeProgess>(json);
        Debug.Log("Highest Score is Loaded");
    }

}
