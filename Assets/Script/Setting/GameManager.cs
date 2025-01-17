using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
  
    public StageList stageList;

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
        var stageData = stageList.stageList.Find(stage => stageID == stage.stageID);
        return stageData;
    }
}
