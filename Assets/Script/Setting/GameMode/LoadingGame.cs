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

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ResetLegacyAnimations();
        }
    }

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
            loadProgress = 0; // Reset tiến trình
            loadingText.text = "0%"; // Hiển thị ban đầu
        }
    }

    private void ResetLegacyAnimations()
    {
        foreach (Animation anim in FindObjectsOfType<Animation>())
        {
            if (anim.clip != null)
            {
                anim.Stop();  // Dừng animation nếu đang chạy
                anim.Play();  // Chạy lại animation từ đầu
            }
        }
    }
}
