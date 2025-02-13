using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }
    public int stageID;
    public int sceneID;
    public string action;
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
        sceneID = data.sceneID;
        SceneManager.LoadScene(sceneID);
    }
    public void LoadSceneByID(int id)
    {
        SceneManager.LoadScene(id);
    }
    public IEnumerator SelectStageScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return StartCoroutine(EnsureGameDataLoaded());
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    private IEnumerator EnsureGameDataLoaded()
    {
        StageSelect stageSelect = FindObjectOfType<StageSelect>();
        stageSelect.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
    }
}
