using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectGameMode : MonoBehaviour
{
    public Button SurivalGameButton;
    public Button BonusGameButton;
    public Button GunTrialButton;

    public Text survivalCount;
    public Text bonusCount;
    public Text gunTrialTimeCount;

    public GameObject statusSurvival;
    public GameObject statusBunus;
    public GameObject statusGuntrial;

    public SurvivalCheck survivalCheckPanel;
    public GameObject bonusCheckPanel;

    private int maxSurivalCount;
    private int maxBonusCount;
    private int survival;
    private int bonus;
    private const int countdownDuration = 12 * 60 * 60;
    private DateTime startTime;
    private bool isCounting = false;

    public void Start()
    {
        SurivalGameButton.onClick.AddListener(LoadSurivalGame);
        BonusGameButton.onClick.AddListener(LoadBonusGame);
        GunTrialButton.onClick.AddListener(LoadGunTrialGame);
        maxBonusCount = 10;
        maxSurivalCount = 10;
        survival = SurvivalMode.Instance.survivalCount;
        bonus = BonusMode.Instance.bonusCount;
        LoadStartTime();
        SetText();
    }
    public void SetText()
    {
        survivalCount.text = $"{survival}/{maxSurivalCount}";
        bonusCount.text = $"{bonus}/{maxBonusCount}";
        int remainingTime = GetRemainingTime();
        gunTrialTimeCount.text = remainingTime > 0 ? FormatTime(remainingTime) : "";

        if (remainingTime > 0)
        {
            StartCoroutine(CheckCountdown());
        }
        CheckStatus();
    }
    public void CheckStatus()
    {
        if (survival == maxSurivalCount)
        {
            statusSurvival.SetActive(true);
        }
        if(bonus == maxBonusCount)
        {
            statusBunus.SetActive(true);
        }
        if(!isCounting)
        {
            statusGuntrial.SetActive(true);
        }
    }
    public void LoadSurivalGame()
    {
        if (survival == maxSurivalCount)
        {
            SceneManager.LoadScene(21);
        }
      else
        {
            survivalCheckPanel.gameObject.SetActive(true);
        }
    }
    public void LoadBonusGame()
    {
        if (bonus == maxBonusCount)
        {
            var randomIndex = Random.Range(22, 23);
            SceneManager.LoadScene(randomIndex);
        }
        else
        {
            bonusCheckPanel.SetActive(true);
        }
    }
    public void LoadGunTrialGame()
    {
        if (isCounting) return;
        SceneManager.LoadScene(24);
        ResetCountdown();
    }
    private void LoadStartTime()
    {
        if (PlayerPrefs.HasKey("startTime"))
        {
            startTime = DateTime.Parse(PlayerPrefs.GetString("startTime"));
            isCounting = true;
        }
        else
        {
            StartCountdown();
        }
    }
    private int GetRemainingTime()
    {
        TimeSpan elapsed = DateTime.Now - startTime;
        return countdownDuration - (int)elapsed.TotalSeconds;
    }
    public void StartCountdown()
    {
        startTime = DateTime.Now;
        PlayerPrefs.SetString("startTime", startTime.ToString());
        PlayerPrefs.Save();
        isCounting = true;
        StartCoroutine(CheckCountdown());
    }
    public void ResetCountdown()
    {
        PlayerPrefs.DeleteKey("startTime");
        isCounting = false;
    }
    private IEnumerator CheckCountdown()
    {
        while (isCounting)
        {
            int remainingTime = GetRemainingTime();
            if (remainingTime <= 0)
            {
                gunTrialTimeCount.text = "";
                isCounting = false;
                CheckStatus();
                yield break;
            }
            gunTrialTimeCount.text = FormatTime(remainingTime);
            yield return new WaitForSeconds(1);
        }
    }
    private string FormatTime(int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
}
