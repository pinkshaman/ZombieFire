using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Button nextArena;
    public Button prevArena;
    public Image arenaImage;
    public Text arenaName;
    public List<StageUi> stageUiList;
    public LoadingGame LoadingPanel;

    private ArenaList arenaList;
    private ArenaProgessList arenaProgessList;
    private int choosedArena = 1;
    private int maxArena;

    public void Start()
    {
        arenaList = StageGameMode.Instance.ReturnArenaList();
        arenaProgessList = StageGameMode.Instance.ReturnArenaProgessList();
        maxArena = arenaList.areraList.Count;
        SetDataArena(choosedArena);
        UpdateButtonState();
        InitButton();
    }
    public void SetDataArena(int arenaIndex)
    {
        var arena = arenaList.areraList[arenaIndex - 1];
        var progess = arenaProgessList.arenaProgressList[arenaIndex - 1];
        arenaImage.sprite = arena.areraImage;
        arenaName.text = $"ARENA {arena.areaNumber}";
        LoadStageSelectData(arena.stageList, progess.stageProgessList);
    }
    public void LoadStageSelectData(StageList stageList, StageProgessList stageProgessList)
    {
        for (int i = 0; i < stageUiList.Count; i++)
        {
            stageUiList[i].SetData(stageList.stageLists[i], stageProgessList.stageProgessLists[i], LoadStage);
        }
    }

    public void InitButton()
    {
        nextArena.onClick.AddListener(() => ChangeArena(1));
        prevArena.onClick.AddListener(() => ChangeArena(-1));
    }
    private void ChangeArena(int index)
    {
        int newArena = choosedArena + index;
        if (newArena >= 1 && newArena <= maxArena && arenaProgessList.arenaProgressList[newArena - 1].isActiveArena)
        {
            choosedArena = newArena;
            SetDataArena(choosedArena);
            UpdateButtonState();
        }
    }
    private void UpdateButtonState()
    {
        var currentArenaProgess = arenaProgessList.arenaProgressList[choosedArena - 1];

        nextArena.interactable = choosedArena < maxArena && arenaProgessList.arenaProgressList[choosedArena].isActiveArena;
        prevArena.interactable = choosedArena > 1 && currentArenaProgess.isActiveArena;
    }
    public virtual void LoadStage(StageUi stageUI)
    {
        Debug.Log($"Data :{stageUI.stageData.sceneID}");
        StageGameMode.Instance.SetCurrentArenaAndStage(stageUI.stageData.stageID);
        LoadingPanel.gameObject.SetActive(true);
        LoadingPanel.StartLoadingScene();
    }
}
