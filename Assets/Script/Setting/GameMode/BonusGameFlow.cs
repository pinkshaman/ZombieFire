using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BonusGameFlow : GameFlow
{
    public BonusStage bonusStage;
    public TimeUpAlert timeUpAlert;
    public Text timerText;
    private float timeRemaining;
    public override void InitData()
    {
        timeRemaining = 120f;

    }
    public override void LoadGamePlay()
    {

        isSpawnDone = false;
        isWaveEnd = false;
        isStageClear = false;
        InitData();
        zombieRepawn.OnZombieClear.AddListener(IsWaveEnd);
        zombieRepawn.OnSpawnDone.AddListener(IsSpawnDone);
        OnStageClear.AddListener(ClearStage);

        StartCoroutine(SpawnByNumberWave());
        StartCoroutine(StartCountdown());
    }
    public override IEnumerator SpawnByNumberWave()
    {
        foreach (var wave in bonusStage.bonusWaves)
        {
            if (wave.waveNumber == 1)
            {
                StartStage();
            }
            else
            {
                PlayAlert(wave.waveNumber);
            }
            zombieRepawn.StartWave(wave);
            yield return new WaitUntil(() => isWaveEnd);
            isWaveEnd = false;

        }
        isStageClear = true;
        OnStageClear.Invoke();
        StopCoroutine("StartCountdown");
    }


    private IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
            yield return null;
        }
        TimeUp();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TimeUp()
    {
        Debug.Log("TimeOut");

        isStageClear = true;
        ClearStage();
    }
    public override void ActiveResultPanel()
    {
        resutlPanel.SetActive(true);
        var result = FindObjectOfType<BnResult>();
        CheckMisson();
    }
    public override void CheckMisson()
    {

    }


}
