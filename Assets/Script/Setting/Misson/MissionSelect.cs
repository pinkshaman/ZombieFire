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

    public AllReward allReward;

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
        var allDailyMisson = GetComponentsInChildren<QuestUi>(rootUiDailyMission);
        var allRepeatMisson = GetComponentsInChildren<QuestUi>(rootUiRepeatMisson);
        var allAchievement = GetComponentsInChildren<AchievementUi>(rootUiAchievement);

        foreach (var daily in allDailyMisson)
        {
            if(daily.missionProgess.isComplete && !daily.missionProgess.isTook)
            {
                allReward.rewardListToShow.Add(daily.missionBase.reward);
            }
        }
        foreach (var repeat in allRepeatMisson)
        {
            if(repeat.missionProgess.isComplete&& !repeat.missionProgess.isTook)
            {
                allReward.rewardListToShow.Add(repeat.missionBase.reward);
            }
        }
        foreach(var achievemnt in allAchievement)
        {
            if(achievemnt.achievementProgess.isComplete&& !achievemnt.achievementProgess.isTook)
            {
                allReward.rewardListToShow.Add(achievemnt.achievementBase.reward);
            }
        }
        allReward.ShowReward();
    }
}
