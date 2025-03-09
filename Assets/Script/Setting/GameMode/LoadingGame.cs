using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public Text loadingText;
    private float loadProgress = 0f;
    private bool isLoading = false;
    public GameObject chooseStage;

    void Update()
    {
        if (isLoading)
        {
            loadProgress += Time.deltaTime * 30;
            if (loadProgress > 100) loadProgress = 100;

            loadingText.text = $"{loadProgress:F0}%";

            if (loadProgress >= 100)
            {
                var sceneID = StageGameMode.Instance.currentStageLoad;
                MySceneManager.Instance.LoadSceneByID(sceneID);
                isLoading = false;
            }
        }
    }

    public void StartLoadingScene()
    {
        chooseStage.gameObject.SetActive(false);
        if (!isLoading)
        {
            isLoading = true;
            loadProgress = 0;
            loadingText.text = "0%";
        }
    }
}
