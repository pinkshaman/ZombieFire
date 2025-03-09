using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrialGameFlow : GameFlow
{
    public GunTrialStage Stage;
    public override void InitData()
    {


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
            zombieRepawn.StartWave(wave);
            yield return new WaitUntil(() => isWaveEnd);
            isWaveEnd = false;

        }
        isStageClear = true;
        OnStageClear.Invoke();
    }
}
