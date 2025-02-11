using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public int stageID;
    

    public void LoadSceneByStageID(int id)
    {
        SceneManager.LoadScene(id);
    }
}
