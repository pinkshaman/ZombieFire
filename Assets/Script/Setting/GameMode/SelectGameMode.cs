using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectGameMode : MonoBehaviour
{
    public Button SurivalGameButton;
    public Button BonusGameButton;
    public Button GunTrialButton;


    public void Start()
    {
        SurivalGameButton.onClick.AddListener(LoadSurivalGame);
        BonusGameButton.onClick.AddListener (LoadBonusGame);
        GunTrialButton.onClick.AddListener (LoadGunTrialGame);
    }

    public void LoadSurivalGame()
    {
        SceneManager.LoadScene(21);
    }
    public void LoadBonusGame()
    {
        var randomIndex = Random.Range(22, 23);
        SceneManager.LoadScene(randomIndex);
    }
    public void LoadGunTrialGame()
    {
        SceneManager.LoadScene(24);
    }
}
