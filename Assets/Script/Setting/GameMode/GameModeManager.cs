using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class GameModeType
{
    public GameType gameType;
    public GameMode gameMode;
    public Button chooseModeButton;
}
public enum GameType
{
    Stage,
    Surival,
    Bonus,
    GunTrial,
}
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }
    public List<GameModeType> gameModeTypes;

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
        InitGameModeButtons();
    }
    private void InitGameModeButtons()
    {
        foreach (var gameModeType in gameModeTypes)
        {
            if (gameModeType.chooseModeButton != null && gameModeType.gameMode != null)
            {
                gameModeType.chooseModeButton.onClick.AddListener(() => gameModeType.gameMode.StartGameMode());
            }
        }
    }
   
}
