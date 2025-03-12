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
    public virtual void LoadGamePlay()
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
    public virtual void InitData()
    {
        stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
    }
    public virtual IEnumerator SpawnByNumberWave()
    {
        foreach (var wave in stage.waveList)
        {
            if (wave.waveNumber == 1)
            {
                StartStage();
            }
            else if(stage.isBossWave)
            {
                break;
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


    public  void IsWaveEnd()
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
        StartSlowMotion();
        clearAlert.gameObject.SetActive(true);
        clearAlert.AlertPlay();
        StartCoroutine(GameEnd());
       
    }
    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(5.0f);
        Time.timeScale = 0;
        BonusMode.Instance.UpdateRequireTimes();
        SurvivalMode.Instance.UpdateRequireTimes();
        ActiveResultPanel();

    }
    public void StartSlowMotion()
    {
        StartCoroutine(SlowMotionEffect());
    }
    private IEnumerator SlowMotionEffect()
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }
    public void StartStage()
    {
        startAlert.gameObject.SetActive(true);
        startAlert.StartAlerts();
    }
    [ContextMenu("ActiveResultPanel")]
    public virtual void ActiveResultPanel()
    {
        resutlPanel.SetActive(true);
        var result = FindObjectOfType<StageResult>();
        string rankClass = result.ReturnRank();
        Debug.Log($"RankClass:{rankClass}");
        StageGameMode.Instance.UpdateDataArenaProgess(stage.stageID, isStageClear, rankClass);
        CheckMisson();
    }

    public virtual void CheckMisson()
    {
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Play, "Game", 1);
        MissonManager.Instance.UpdateMissionProgress(MissionRequireType.Play, "Game", 1);
    }
}
