

using System.Collections.Generic;
using System;
using UnityEngine;



[Serializable]
public class StageProgess
{
    public float stageID;
    public bool isComplete;
    public string stageRank;

    public StageProgess(float stageID, bool isComplete, string stageRank)
    {
        this.stageID = stageID;
        this.isComplete = isComplete;
        this.stageRank = stageRank;
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
    public StageProgessList stageProgessList;
}
[Serializable]
public class ArenaProgessList
{
    public List<ArenaProgess> arenaProgressList;
}


public class StageGameMode : GameMode
{
    public ArenaList arenaListed;
    public ArenaProgessList arenaProgessListed;
    public StageUi stageUiPrefabs;
    public Transform rootUI;
    public GameObject selectStagePanel;
    public void Start()
    {
        LoadStageData();
    }
    public override void StartGameMode()
    {
        selectStagePanel.SetActive(true);
        CreateStage(1);
    }
    public Stage LoadData(float stageID, int arena)
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == arena);
        var stageData = arenaData.stageList.stageLists.Find(stageData => stageData.stageID == stageID);
        return stageData;
    }
    public void CreateStage(int arena)
    {
        var arenaData = arenaListed.areraList.Find(arenaData => arenaData.areaNumber == arena);
        var arenaProgess = arenaProgessListed.arenaProgressList.Find(arenaProgess => arenaProgess.arenaNumber == arena);

        foreach (var stage in arenaData.stageList.stageLists)
        {
            var stageProgessData = arenaProgess.stageProgessList.stageProgessLists.Find(stageProgessData => stageProgessData.stageID == stage.stageID);
            if (stageProgessData == null)
            {
                IntNewProgessStage(stage, arenaProgess);
            }
            var stageSelect = Instantiate(stageUiPrefabs, rootUI);
            stageSelect.SetData(stage, stageProgessData);
        }
    }
    public void IntNewProgessStage(Stage stage, ArenaProgess arenaProgess)
    {
        arenaProgess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null));
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
            var arenaprogess = arenaProgessListed.arenaProgressList.Find(ap => ap.arenaNumber == arenaData.areaNumber);
            if (arenaprogess == null)
            {
                arenaprogess = new ArenaProgess
                {
                    arenaNumber = arenaData.areaNumber,
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
                if (!arenaprogess.stageProgessList.stageProgessLists.Exists(sp => sp.stageID == stage.stageID))
                {
                    arenaprogess.stageProgessList.stageProgessLists.Add(new StageProgess(stage.stageID, false, null));
                }
            }
        }

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
