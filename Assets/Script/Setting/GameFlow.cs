using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public float stageID;
    public Stage stage;
    public ZombieRepawn zombieRepawn;
    public WarningAlert warningAlert;

    public bool isWaveEnd;
    public bool isSpawnDone;

    public void Start()
    {
        isSpawnDone = false;
        isWaveEnd = false;
        stage = GameManager.Instance.LoadStageData(stageID);
        StartCoroutine(SpawnByNumberWave());
        zombieRepawn.OnZombieClear.AddListener(IsWaveEnd);
        zombieRepawn.OnSpawnDone.AddListener(IsSpawnDone);
    }

    public void SpawnEnemy(GameObject zombie, int quatity)
    {
        StartCoroutine(zombieRepawn.SpawnZombieByTime(zombie, quatity));
    }
    public void SpawnWave(float waveNumber)
    {
        BaseWave baseWave = stage.waveList.Find(baseWave => baseWave.waveNumber == waveNumber);

        foreach (var Group in baseWave.zombieList)
        {
            SpawnEnemy(Group.zombie, Group.quatity);
            if(isSpawnDone)
            {
                break;
            }
        }        
    }
    public IEnumerator SpawnByNumberWave()
    {
        foreach (var number in stage.waveList)
        {
            SpawnWave(number.waveNumber);
            PlayAlert(number.waveNumber);
            yield return new WaitUntil(() => isWaveEnd); 
            isWaveEnd = false ;
        }
    }
    public void IsWaveEnd()
    {
        isWaveEnd = true;
    }
    public void IsSpawnDone()
    {
        isSpawnDone = true;
    }
    public void PlayAlert(float number)
    {
        warningAlert.gameObject.SetActive(true);
        warningAlert.WarningPlay(number);
    }
    
}
