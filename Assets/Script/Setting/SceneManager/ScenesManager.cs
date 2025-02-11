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
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    [ContextMenu("LoadSceneByStageID")]
    public void LoadSceneByStageID(Stage data)
    {
        SceneManager.LoadScene(data.sceneID); 
        StageGameMode.Instance.SetCurrentStage(data.stageID);
    }
}
