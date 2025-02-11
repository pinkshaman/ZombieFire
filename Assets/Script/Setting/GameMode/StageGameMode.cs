

using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;



[Serializable]
public class StageProgess
{
    public float stageID;
    public bool isComplete;
    public string stageRank;
    public bool isCanPlay;

    public StageProgess(float stageID, bool isComplete, string stageRank,bool isCanPlay)
    {
        this.stageID = stageID;
        this.isComplete = isComplete;
        this.stageRank = stageRank;
        this.isCanPlay = isCanPlay;
    }
}
[Serializable]
public class StageProgessList
{
    public List<StageProgess> stageProgessLists;
}
[Serializable]
public class ArenaProgess
{
    public int arenaNumber;
    public bool isActiveArena;
    public StageProgessList stageProgessList;
}
[Serializable]
public class ArenaProgessList
{
    public List<ArenaProgess> arenaProgressList;
}


public class StageGameMode : MonoBehaviour
{
    public static StageGameMode Instance { get; private set; }
    public ArenaList arenaListed;
    public ArenaProgessList arenaProgessListed;
    public GameObject selectStagePanel;
    public StageSelect stageSelect;
    private int currentArena = 1;
    public float currentStage = 0;
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
        LoadStageData();
        InitArenaProgessData();
        stageSelect.InitButton(ChangeArena);
    }

    public void SetCurrentStage(float index)
    {
        currentStage = index;
    }
    public void ChangeArena(int index  )
    {
        int newArena = currentArena + index;
        if (IsValidArena(newArena))
        {
            currentArena = newArena;
            CreateArena(currentArena);
        }
    }
    public bool IsValidArena(int arena)
    {
        return arena >= 1 && arena <= arenaListed.areraList.Count;
    }
    public void UpdateArena()
    {
        CreateArena(currentArena);
        stageSelect.UpdateButtonState(currentArena, arenaListed.areraList.Count);
    }
    public  void StartGameMode()
    {
        selectStagePanel.SetActive(true);
        CreateArena(currentArena);
    }

    public Stage LoadData(/*int arenaID, int stageID*/)
    {
        Debug.Log($"CurrenArena : {currentArena}");
        Debug.Log($"CurrentStage: {currentStage}");
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == currentArena);
        var stageData = arenaData.stageList.stageLists.Find(stageData => stageData.stageID == currentStage);
        return stageData;
    }
    public void CreateArena(int arena)
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == arena);
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaProgess => arenaProgess.arenaNumber == arena);

        stageSelect.LoadStageSelectData(arenaData.stageList.stageLists, arenaProgess.stageProgessList.stageProgessLists, arenaData);
        CheckAndUnlockNextArena(arena);
    }
    public void IntNewProgessStage(Stage stage, ArenaProgess arenaProgess)
    {
        arenaProgess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null,false));
        SaveStageData();
    }

    [ContextMenu("InitArenaProgessData")]
    public void InitArenaProgessData()
    {
        if (arenaProgessListed == null)
        {
            arenaProgessListed = new ArenaProgessList { arenaProgressList = new List<ArenaProgess>() };
        }

        foreach (var arenaData in arenaListed.areraList)
        {
            // Kiểm tra xem ArenaProgess có tồn tại không, nếu không thì tạo mới
            var arenaprogess = arenaProgessListed.arenaProgressList.Find(arenaprogess => arenaprogess.arenaNumber == arenaData.areaNumber);
            if (arenaprogess == null)
            {
                arenaprogess = new ArenaProgess
                {
                    arenaNumber = arenaData.areaNumber,
                    isActiveArena = (arenaData.areaNumber == 1),
                    stageProgessList = new StageProgessList { stageProgessLists = new List<StageProgess>() }
                };

                arenaProgessListed.arenaProgressList.Add(arenaprogess);
            }

            // Đảm bảo stageProgessList không null
            if (arenaprogess.stageProgessList == null)
            {
                arenaprogess.stageProgessList = new StageProgessList { stageProgessLists = new List<StageProgess>() };
            }

            foreach (var stage in arenaData.stageList.stageLists)
            {
                // Kiểm tra xem stage đã tồn tại chưa trước khi thêm
                if (!arenaprogess.stageProgessList.stageProgessLists.Exists(stageProgess => stageProgess.stageID == stage.stageID))
                {
                    arenaprogess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null, false));
                }
            }
        }

        SaveStageData();
    }
    public void CheckAndUnlockNextArena(int currentArenaNumber)
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaprogess => arenaprogess.arenaNumber == currentArenaNumber);
        if (arenaProgess == null) return;

        bool allStagesCompleted = arenaProgess.stageProgessList.stageProgessLists.All(stage => stage.isComplete);

        if (allStagesCompleted)
        {
            var nextArena = arenaProgessListed.arenaProgressList.Find(arena => arena.arenaNumber == currentArenaNumber + 1);
            if (nextArena != null)
            {
                nextArena.isActiveArena = true;
                Debug.Log($"Arena {nextArena.arenaNumber} isActived");
                SaveStageData();
            }
        }
    }


    [ContextMenu("SaveStageData")]
    public void SaveStageData()
    {
        var value = JsonUtility.ToJson(arenaProgessListed);
        PlayerPrefs.SetString(nameof(arenaProgessListed), value);
        PlayerPrefs.Save();
    }
    [ContextMenu("LoadStageData")]
    public void LoadStageData()
    {
        var defaultValue = JsonUtility.ToJson(arenaProgessListed);
        var json = PlayerPrefs.GetString(nameof(arenaProgessListed), defaultValue);
        arenaProgessListed = JsonUtility.FromJson<ArenaProgessList>(json);
        Debug.Log("ArenVsStage are Loaded");
    }



}
