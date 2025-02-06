using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class StageProgess
{
    public float stageID;
    public bool isComplete;
    public string stageRank;   
}
[Serializable]
public class StageProgessList
{
    public List<StageProgess> stageProgessLists;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StageList stageList;
    public StageProgessList stageProgessList;
    public GameMode gameMode;
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
       
    }
    public Stage LoadStageData(float stageID)
    {
        var stageData = stageList.stageLists.Find(stage => stageID == stage.stageID);
        return stageData;
    }
    public void CreateStage()
    {
        foreach(var stage in stageList.stageLists)
        {
            var stageProgessData = stageProgessList.stageProgessLists.Find(stageProgessData =>stageProgessData.stageID == stage.stageID);

        }
    }
}
