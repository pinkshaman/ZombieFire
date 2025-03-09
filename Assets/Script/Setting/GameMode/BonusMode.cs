using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BonusMode : MonoBehaviour
{
    public static BonusMode Instance { get; private set; }
    public int bonusCount;
  
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
        LoadBonusCount();
    }
    public void UpdateRequireTimes()
    {
        if (bonusCount >= 10) return;
        bonusCount++;
        SaveBonusCount();
    }
    [ContextMenu("SaveBonusCount")]
    public void SaveBonusCount()
    {
        PlayerPrefs.SetString("bonusCount", bonusCount.ToString());
        PlayerPrefs.Save();
    }
    public void LoadBonusCount()
    {
        if (PlayerPrefs.HasKey("bonusCount"))
        {
            bonusCount = int.Parse(PlayerPrefs.GetString("bonusCount"));
        }
        else
        {
            bonusCount = 0;
            SaveBonusCount();
        }
    }
}
