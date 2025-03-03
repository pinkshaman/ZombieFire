using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MissonManager : MonoBehaviour
{
    public static MissonManager Instance { get; private set; }
    public MissonProgessList missionProgessList;
    public MissionDaily missionDaily;
    public MissonRepeat missionRepeat;

    public AchievementList achievementList;
    public AchievementProgessList achievementProgessList;
    private const string LastResetKey = "LastDailyReset";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        CheckAndResetDailyMission();
        LoadMissionProgess();
        LoadAchievementProgess();
        CreateAchievementList();
        CreateMissionList();
    }

    [ContextMenu("CreateMissionList")]
    public void CreateMissionList()
    {
        if (missionProgessList == null)
            missionProgessList = new MissonProgessList();

        if (missionProgessList.missionProgessList == null)
            missionProgessList.missionProgessList = new List<MissionProgess>();

        AddMissions(missionDaily.missionList, MissionType.Daily);
        AddMissions(missionRepeat.missionList, MissionType.Repeat);

        SaveMissionProgess();
    }
    private void AddMissions(List<MissionBase> missionList, MissionType type)
    {
        foreach (var mission in missionList)
        {
            if (!missionProgessList.missionProgessList.Exists(missionProgess => missionProgess.missionProgessID == mission.missionID && missionProgess.missionType == type))
            {
                missionProgessList.missionProgessList.Add(new MissionProgess(
                    mission.missionID, type, mission.missionRequireType, false, 0, mission.tagertName));
            }
        }
    }
    public void UpdateMissionProgress(MissionRequireType requireType, string targetName, int amount)
    {
        foreach (var progress in missionProgessList.missionProgessList)
        {
            if (progress.missinRequireType == requireType && progress.targetName == targetName && !progress.isComplete)
            {
                progress.missionProgessRequire += amount;
                MissionBase mission = GetMissionByID(progress.missionProgessID);

                if (mission != null && progress.missionProgessRequire >= mission.missionRequire)
                {
                    progress.isComplete = true;
                }
            }
        }
        SaveMissionProgess();
    }


    public MissionBase GetMissionByID(int missionID)
    {
        foreach (var mission in missionDaily.missionList)
            if (mission.missionID == missionID) return mission;

        foreach (var mission in missionRepeat.missionList)
            if (mission.missionID == missionID) return mission;

        return null;
    }

    public void UpdateDataProgess(MissionProgess newdata)
    {
        var data = missionProgessList.missionProgessList.Find(d => d.missionProgessID == newdata.missionProgessID && d.missionType == newdata.missionType);
        if (data != null)
        {
            data = newdata;
            SaveMissionProgess();
        }
    }

    public void CheckAndResetDailyMission()
    {
        string lastResetTimeStr = PlayerPrefs.GetString(LastResetKey, "");
        if (!string.IsNullOrEmpty(lastResetTimeStr))
        {
            DateTime lastResetTime = DateTime.Parse(lastResetTimeStr);
            if ((DateTime.Now - lastResetTime).TotalHours < 24)
            {
                return;
            }
        }

        ResetDailyMission();
    }

    public void ResetDailyMission()
    {
        missionProgessList.missionProgessList.RemoveAll(m => m.missionType == MissionType.Daily);
        AddMissions(missionDaily.missionList, MissionType.Daily);

        SaveMissionProgess();
        PlayerPrefs.SetString(LastResetKey, DateTime.Now.ToString());
        PlayerPrefs.Save();

    }

    [ContextMenu("SaveMissionProgess")]
    public void SaveMissionProgess()
    {
        var value = JsonUtility.ToJson(missionProgessList);
        PlayerPrefs.SetString(nameof(missionProgessList), value);
        PlayerPrefs.Save();
    }

    [ContextMenu("LoadMissionProgess")]
    public void LoadMissionProgess()
    {
        var defaultValue = JsonUtility.ToJson(missionProgessList);
        var json = PlayerPrefs.GetString(nameof(missionProgessList), defaultValue);
        missionProgessList = JsonUtility.FromJson<MissonProgessList>(json);
        Debug.Log("Mission Progress is Loaded");
    }

    //-------------------------------------//


    [ContextMenu("CreateAchievementList")]
    public void CreateAchievementList()
    {
        if (achievementProgessList == null)
        {
            achievementProgessList = new AchievementProgessList();
        }
        if (achievementProgessList.achivementProgessList == null)
        {
            achievementProgessList.achivementProgessList = new List<AchievementProgess>();
        }

        foreach (var achievement in achievementList.achievementList)
        {
            if (!achievementProgessList.achivementProgessList.Any(p => p.progessID == achievement.id))
            {
                AchievementProgess newProgress = new AchievementProgess(
                    achievement.id,
                    achievement.achievementRequireType,
                    0,
                    achievement.targetAchievement,
                    false,
                    false
                );

                achievementProgessList.achivementProgessList.Add(newProgress);
                Debug.Log($"Added Achievement: {achievement.achievementName}");
            }
        }

        SaveAchievementProgess();
    }

    public void UpdateAchievementProgessData(AchievementProgess progess)
    {
        var data = achievementProgessList.achivementProgessList.Find(data => data.progessID == progess.progessID && data.achievementRequireType == progess.achievementRequireType);
        data = progess;
        SaveAchievementProgess();
    }

    public void UpdateAchievementProgess(MissionRequireType type, string progessTarget, int tagetCount)
    {
        foreach (var progress in achievementProgessList.achivementProgessList)
        {
            var achievement = achievementList.achievementList.FirstOrDefault(a => a.id == progress.progessID);
            if (achievement == null || progress.isComplete) continue;

            if (achievement.achievementRequireType == type && (achievement.targetAchievement == progessTarget || type == MissionRequireType.Play))
            {
                progress.currenProgess += (type == MissionRequireType.Play) ? 1 : tagetCount;

                if (progress.currenProgess >= achievement.achievementRequire)
                {
                    progress.isComplete = true;
                }
            }
        }
        SaveAchievementProgess();
    }



    [ContextMenu("SaveAchievementProgess")]
    public void SaveAchievementProgess()
    {
        var value = JsonUtility.ToJson(achievementProgessList);
        PlayerPrefs.SetString(nameof(achievementProgessList), value);
        PlayerPrefs.Save();
    }

    [ContextMenu("LoadAchievementProgess")]
    public void LoadAchievementProgess()
    {
        var defaultValue = JsonUtility.ToJson(achievementProgessList);
        var json = PlayerPrefs.GetString(nameof(achievementProgessList), defaultValue);
        achievementProgessList = JsonUtility.FromJson<AchievementProgessList>(json);
        Debug.Log("Achievement Progress is Loaded");
    }
}


