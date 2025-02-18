using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public Text loadingText;
    public int sceneID;

    
    public IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(StageGameMode.Instance.currentStageLoad);
        asyncLoad.allowSceneActivation = false; 

        float progress = 0;
        while (!asyncLoad.isDone)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f) * 100;
            loadingText.text = $"{progress:F0}%";

            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "99%";

                yield return StartCoroutine(EnsureGameDataLoaded());
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator EnsureGameDataLoaded()
    {    


        loadingText.text = "100%";
        yield return new WaitForSeconds(0.5f);
    }
}
