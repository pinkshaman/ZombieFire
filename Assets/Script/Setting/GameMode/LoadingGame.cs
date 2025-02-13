using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public Text loadingText;

    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadGameScene());
    }

    public IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false; // Không tự động chuyển Scene

        float progress = 0;
        while (!asyncLoad.isDone)
        {
            // Lấy tiến trình Load (0 - 0.9), scale về 1 - 100%
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f) * 100;
            loadingText.text = $"{progress:F0}%";

            // Nếu đã load xong (đạt 90%)
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "99%";

                // Đảm bảo dữ liệu đã tải xong
                yield return StartCoroutine(EnsureGameDataLoaded());

                // Chuyển Scene
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator EnsureGameDataLoaded()
    {    
        // Đợi 1 chút để UI cập nhật hết (nếu cần)
        yield return new WaitForSeconds(0.5f);
        loadingText.text = "100%";
        yield return new WaitForSeconds(0.5f);
    }
}
