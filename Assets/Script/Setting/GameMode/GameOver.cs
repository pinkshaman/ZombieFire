using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public AudioSource gameoverSource;
    public Button ExitToMainMenuButton;
    public Button ContinueButton;
    public Animation anim;
    private int priceToRespawn;
    private int respawnTime;

    public void Start()
    {
        priceToRespawn = 10;
        respawnTime = 0;
        gameoverSource.Play();
        ExitToMainMenuButton.onClick.AddListener(OnButtonExitClick);
        ContinueButton.onClick.AddListener(OnContinuteButtonClick);
    }

    public void OnButtonExitClick()
    {
        MySceneManager.Instance.LoadSceneByID(0);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        gameoverSource.Play();
        ContinueButton.interactable = false;
        anim.Play();
    }


    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSecondsRealtime(anim.clip.length);
        EnableButtons();
    }

    public void EnableButtons()
    {
        ContinueButton.interactable = respawnTime < 2;
        ExitToMainMenuButton.interactable = true;
    }

    public void OnContinuteButtonClick()
    {
        var playerData = PlayerManager.Instance.playerData;
        if (respawnTime < 2 && playerData.gold >= priceToRespawn)
        {
            playerData.gold -= priceToRespawn;
            PlayerManager.Instance.UpdatePlayerData(playerData);
            respawnTime++;
            priceToRespawn *= 2;

            if (respawnTime >= 2)
            {
                ContinueButton.interactable = false;
            }

            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
