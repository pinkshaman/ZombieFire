

using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;



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
    public GameObject selectStagePanel;
    public StageSelect stageSelect;
    public int currentArenaLoad = 1;
    public int currentStageLoad = 0;
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
    }
    public void SetCurrentArenaAndStage(int stage)
    {
        currentStageLoad = stage;

    }
    public Stage ReturnCurrentStageforPlay()
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == currentArenaLoad);
        var stageData = arenaData.stageList.stageLists.Find(stageData => stageData.stageID == currentStageLoad);
        return stageData;
    }
    public int ReturnArenaList()
    {
        return arenaListed.areraList.Count;
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
    public void SaveProgessArena(int arenaIndex)
    {
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaprogess => arenaprogess.arenaNumber == arenaIndex);
        arenaProgess.isActiveArena = true;
        Debug.Log($"Arena {arenaIndex} isComplete");
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
