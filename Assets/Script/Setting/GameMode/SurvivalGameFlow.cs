using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalGameFlow : GameFlow
{
    public SurvivalStage Stage;
    private int highestWave;
    public PlayerHealth playerHealth;
    public Text waveText;
    public override void InitData()
    {
        Stage = SurvivalMode.Instance.survivalStage;
        playerHealth.onDie.AddListener(ActiveResultPanel);
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
    }
    public override IEnumerator SpawnByNumberWave()
    {
        foreach (var wave in Stage.waveList)
        {
            if (wave.waveNumber == 1)
            {
                StartStage();
            }
            else
            {
                PlayAlert(wave.waveNumber);
            }
            waveText.text = wave.waveNumber.ToString();
            zombieRepawn.StartWave(wave);

            yield return new WaitUntil(() => isWaveEnd);
            isWaveEnd = false;
            highestWave = wave.waveNumber;
        }
        isStageClear = true;
        OnStageClear.Invoke();
    }
    public override void ActiveResultPanel()
    {
        resutlPanel.SetActive(true);
    }
    public int ReturnWave()
    {
        return highestWave;
    }
}
