

using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;



[Serializable]
public class StageProgess
{
    public int stageID;
    public bool isComplete;
    public string stageRank;
    public bool isCanPlay;

    public StageProgess(int stageID, bool isComplete, string stageRank, bool isCanPlay)
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
    public int currentArenaLoad = 1;
    public int currentStageLoad = 0;
    private int highestStageUnocked;
    private int highestArenaUnlocked;
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
        LoadStageData();
        InitArenaProgessData();
        DefineHighestArenaUnlocked();
    }
    public void SetCurrentArenaAndStage(int stage)
    {
        currentStageLoad = stage;
    }
    public void DefineHighestArenaUnlocked()
    {
        var activeArenas = arenaProgessListed.arenaProgressList
       .Where(arena => arena.isActiveArena)
       .OrderByDescending(arena => arena.arenaNumber)
       .FirstOrDefault();
        highestArenaUnlocked = activeArenas.arenaNumber;
        activeArenas.stageProgessList.stageProgessLists[0].isCanPlay = true;
    }
    public Stage ReturnCurrentStageforPlay()
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == currentArenaLoad);
        var stageData = arenaData.stageList.stageLists.Find(stageData => stageData.stageID == currentStageLoad);
        return stageData;
    }
    public StageProgess ReturnCurrentStageProgessForPlay()
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaProgess => arenaProgess.arenaNumber == currentArenaLoad);
        var stageProgess = arenaProgess.stageProgessList.stageProgessLists.Find(stageProgess => stageProgess.stageID == currentStageLoad);
        return stageProgess;
    }
    public ArenaList ReturnArenaList()
    {
        return arenaListed;
    }
    public ArenaProgessList ReturnArenaProgessList()
    {
        return arenaProgessListed;
    }
    public Arena ReturnArena(int arena)
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == arena);
        return arenaData;
    }
    public ArenaProgess ReturnProgessArena(int arena)
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaProgess => arenaProgess.arenaNumber == arena);
        return arenaProgess;
    }
    public void UpdateRankStageProgess(string rank)
    {

    }
    public void UpdateDataArenaProgess(int stage, bool isClear, string rankText)
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaProgess => arenaProgess.arenaNumber == currentArenaLoad);
        var stageData = arenaProgess.stageProgessList.stageProgessLists.Find(stageData => stageData.stageID == stage);
        stageData.isComplete = isClear;
        stageData.stageRank = rankText;
        UnlockNextState(currentStageLoad + 1, arenaProgess);
        if (stageData.stageID == 20 && stageData.isComplete == true)
        {
            ActiveNextArena(currentArenaLoad + 1);
        }
        SaveStageData();
    }
    public void IntNewProgessStage(Stage stage, ArenaProgess arenaProgess)
    {
        arenaProgess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null, false));
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

            if (arenaprogess.stageProgessList == null)
            {
                arenaprogess.stageProgessList = new StageProgessList { stageProgessLists = new List<StageProgess>() };
            }

            foreach (var stage in arenaData.stageList.stageLists)
            {
                if (!arenaprogess.stageProgessList.stageProgessLists.Exists(stageProgess => stageProgess.stageID == stage.stageID))
                {
                    arenaprogess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null, false));

                }
            }
        }

        SaveStageData();
    }
    public void ActiveNextArena(int arenaIndex)
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaprogess => arenaprogess.arenaNumber == arenaIndex);
        var arenaCount = arenaListed.areraList.Count;
        if (arenaIndex < highestArenaUnlocked|| arenaIndex> arenaCount ) return;
        arenaProgess.isActiveArena = true;
        Debug.Log($"Arena {arenaIndex - 1} isComplete");
        SaveStageData();
    }
    public void UnlockNextState(int stageIndex, ArenaProgess arenaProgess)
    {
        var stageCount = arenaProgess.stageProgessList.stageProgessLists.Count;
        var stageData = arenaProgess.stageProgessList.stageProgessLists.Find(stageData => stageData.stageID == stageIndex);
        var completedStage = arenaProgess.stageProgessList.stageProgessLists
            .Where(stage => stage.isComplete)
            .OrderByDescending(stage => stage.stageID)
            .FirstOrDefault();
        highestStageUnocked = completedStage.stageID;
        if (stageIndex <= highestStageUnocked || stageIndex > stageCount) return;
        stageData.isCanPlay = true;
        SaveStageData();
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
