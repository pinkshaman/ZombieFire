using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelect : MonoBehaviour
{
    public Transform rootUiRepeatMisson;
    public Transform rootUiDailyMission;
    public Transform rootUiAchievement;
    public QuestUi questPrefabsUi;
    public AchievementUi AchievementPrefabsUi;
    public Button getAllRewardButton;

    public void Start()
    {
        CreateAllMission();
        CreateAllAchievement();
        getAllRewardButton.onClick.AddListener(TakeAllReward);
    }
    public void CreateAllMission()
    {
        var AllMission = MissonManager.Instance.missionProgessDictionary;
        foreach (var mission in AllMission)
        {
            var (missionType, missionID) = mission.Key;
            var (progress, missionBase) = mission.Value;

            var missionUiInstance = Instantiate(questPrefabsUi);
            missionUiInstance.SetMissionData(missionBase, progress);

            if (missionType == MissionType.Daily)
            {
                missionUiInstance.transform.SetParent(rootUiDailyMission, false);
            }
            else if (missionType == MissionType.Repeat)
            {
                missionUiInstance.transform.SetParent(rootUiRepeatMisson, false);
            }
        }
    }
    public void CreateAllAchievement()
    {
        var AllAchievement = MissonManager.Instance.achievementDictionary;
        foreach (var achievement in AllAchievement)
        {
            var key = achievement.Key;
            var (achievementBase, progess) = achievement.Value;

            var achievementUIInstance = Instantiate(AchievementPrefabsUi,rootUiAchievement);
           achievementUIInstance.SetAchievementData(achievementBase, progess);
        }
    }
    public void TakeAllReward()
    {
      
    }
    public void TakeAllMission()
    {
        
    }

}
