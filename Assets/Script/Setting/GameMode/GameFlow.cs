using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameFlow : MonoBehaviour
{
    public Stage stage;
    public ZombieRepawn zombieRepawn;
    public WaveAlert waveAlert;
    public StartAlert startAlert;
    public ClearAlert clearAlert;
    public bool isWaveEnd;
    public bool isSpawnDone;
    public bool isStageClear;
    public UnityEvent OnStageClear;

    public GameObject resutlPanel;

    public void Start()
    {
        LoadGamePlay();
    }
    public void LoadGamePlay()
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
    public void InitData()
    {
        stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
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
            if (isSpawnDone)
            {
                break;
            }
        }
    }
    public IEnumerator SpawnByNumberWave()
    {
        foreach (var number in stage.waveList)
        {
            if (number.waveNumber == 1)
            {
                StartStage();
            }
            else
            {
                PlayAlert(number.waveNumber);
            }
            SpawnWave(number.waveNumber);

            yield return new WaitUntil(() => isWaveEnd);
            isWaveEnd = false;
        }
        isStageClear = true;
        OnStageClear.Invoke();
    }
    public void IsWaveEnd()
    {
        isWaveEnd = true;
    }
    public void IsSpawnDone()
    {
        isSpawnDone = true;
    }
 
    public void PlayAlert(int number)
    {
        waveAlert.gameObject.SetActive(true);
        waveAlert.WaveWarning(number);
    }
    public void ClearStage()
    {
        if (!isStageClear) return;

        clearAlert.gameObject.SetActive(true);
        clearAlert.AlertPlay();
        ActiveResultPanel();
        //Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;         
    }
    public void GameEnd()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartStage()
    {
        startAlert.gameObject.SetActive(true);
        startAlert.StartAlerts();
    }
    [ContextMenu("ActiveResultPanel")]
    public void ActiveResultPanel()
    {
        resutlPanel.SetActive(true);
        var result = FindObjectOfType<Result>();
        string rankClass = result.ReturnRank();
        Debug.Log($"RankClass:{rankClass}");
        StageGameMode.Instance.UpdateDataArenaProgess(stage.stageID, isStageClear,rankClass);

    }
}
