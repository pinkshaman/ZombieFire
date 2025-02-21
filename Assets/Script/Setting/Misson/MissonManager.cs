using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissonManager : MonoBehaviour
{
    public static MissonManager Instance { get; private set; }
    public MissonProgessList missionProgessList;
    public Dictionary<(MissionType, int), (MissionProgess progress, MissionBase mission)> missionProgessDictionary = new Dictionary<(MissionType, int), (MissionProgess, MissionBase)>();
    public MissionDaily missionDaily;
    public MissonRepeat missionRepeat;

    public AchievementList achievementList;
    public AchievementProgessList achievementProgessList;

    public Dictionary<int, (AchievementBase achievementBase, AchievementProgess achievementtProgess)> achievementDictionary = new Dictionary<int, (AchievementBase, AchievementProgess)>();

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
        CreateDictionary();
        CreateAchievementDictionary();
    }

    [ContextMenu("CreateDictionary")]
    public void CreateDictionary()
    {

        if (missionProgessList == null)
        {
            missionProgessList = new MissonProgessList();
        }
        if (missionProgessList.missionProgessList == null)
        {
            missionProgessList.missionProgessList = new List<MissionProgess>();
        }
        if (missionProgessDictionary == null)
        {
            missionProgessDictionary = new Dictionary<(MissionType, int), (MissionProgess, MissionBase)>();
        }
        AddMissionsToDictionary(missionDaily);
        AddMissionsToDictionary(missionRepeat);
        Debug.Log($"Dictionary is Created with {missionProgessDictionary.Count} missions.");
        SaveMissionProgess();
    }

    private void AddMissionsToDictionary(Mission mission)
    {
        foreach (var missionBase in mission.missionList)
        {
            var key = (mission.missionType, missionBase.missionID);

            if (!missionProgessDictionary.ContainsKey(key))
            {
                MissionProgess newProgress = new MissionProgess(
                    missionBase.missionID,
                    mission.missionType,
                    missionBase.missionRequireType,
                    false,
                    0,
                    missionBase.tagertName
                );

                missionProgessList.missionProgessList.Add(newProgress);
                missionProgessDictionary[key] = (newProgress, missionBase);

                Debug.Log($"Created ProgressMission: {mission.missionType}, {missionBase.missionName}, {missionBase.missionRequireType}, {missionBase.missionRequire} - {missionBase.tagertName}");
            }
        }
    }

    public void UpdateMissionProgress(MissionRequireType requireType, string targetName, int amount)
    {
        foreach (var entry in missionProgessDictionary)
        {
            var (missionType, missionID) = entry.Key;
            var (progress, mission) = entry.Value;

            if (progress.missinRequireType == requireType && progress.targetName == targetName && !progress.isComplete)
            {
                progress.missionProgessRequire += amount;

                if (progress.missionProgessRequire >= mission.missionRequire)
                {
                    progress.isComplete = true;
                    Debug.Log($"Mission Updated: {mission.missionName} ({progress.missionProgessRequire}/{mission.missionRequire}) - {mission.tagertName}");

                    CompleteMission(missionType, missionID);
                }
            }
        }
    }

    private void CompleteMission(MissionType missionType, int missionID)
    {
        var key = (missionType, missionID);
        if (missionProgessDictionary.ContainsKey(key))
        {
            var (progress, mission) = missionProgessDictionary[key];

            if (!progress.isComplete)
            {
                progress.isComplete = true;
                Debug.Log($"Mission Complete: {mission.missionName}");
            }
        }
    }

    public (MissionProgess progress, MissionBase mission)? GetMissionData(MissionType missionType, int missionID)
    {
        var key = (missionType, missionID);
        if (missionProgessDictionary.ContainsKey(key))
        {
            return missionProgessDictionary[key];
        }

        return null;
    }

    public void UpdateDataProgess(MissionProgess newdata)
    {
        var data = missionProgessList.missionProgessList.Find(d => d.missionProgessID == newdata.missionProgessID && d.missionType == newdata.missionType);
        if (data != null)
        {
            data.missionProgessRequire = newdata.missionProgessRequire;
            data.isComplete = newdata.isComplete;
            SaveMissionProgess();
        }
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


    [ContextMenu("CreateAchievementDictionary")]
    public void CreateAchievementDictionary()
    {
        if (achievementProgessList == null)
        {
            achievementProgessList = new AchievementProgessList();
        }
        if (achievementProgessList.achivementProgessList == null)
        {
            achievementProgessList.achivementProgessList = new List<AchievementProgess>();
        }
        if (achievementDictionary == null)
        {
            achievementDictionary = new Dictionary<int, (AchievementBase, AchievementProgess)>();
        }
        AddAchievementToDictionary();
    }
    public void AddAchievementToDictionary()
    {
        foreach (var achievement in achievementList.achievementList)
        {
            var key = achievement.id;

            if (!achievementDictionary.ContainsKey(key))
            {
                AchievementProgess newProgress = new AchievementProgess(achievement.id, achievement.targetAchievement, false, false);
                achievementProgessList.achivementProgessList.Add(newProgress);
                achievementDictionary[key] = (achievement, newProgress);

            }
            SaveAchievementProgess();
        }
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
        Debug.Log("Mission Progress is Loaded");
    }
}


