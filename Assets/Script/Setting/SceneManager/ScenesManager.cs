using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; }
    public int stageID;

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

    [ContextMenu("LoadSceneByStageID")]
    public void LoadSceneByStageID(Stage data)
    { 
        StageGameMode.Instance.SetCurrentStage(data.stageID);
        SceneManager.LoadScene(data.sceneID);
    }
}
