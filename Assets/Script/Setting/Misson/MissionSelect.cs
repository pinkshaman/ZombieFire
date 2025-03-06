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
        var missionList = MissonManager.Instance.missionProgessList.missionProgessList;

        foreach (var progress in missionList)
        {
            MissionBase missionBase = MissonManager.Instance.GetMissionByID(progress.missionProgessID);
            if (missionBase == null) continue;

            var missionUi = Instantiate(questPrefabsUi);
            missionUi.SetMissionData(missionBase, progress);

            missionUi.transform.SetParent(progress.missionType == MissionType.Daily ? rootUiDailyMission : rootUiRepeatMisson, false);
        }
    }

    public void CreateAllAchievement()
    {
        var achievementProgressList = MissonManager.Instance.achievementProgessList.achivementProgessList;
        var achievementList = MissonManager.Instance.achievementList.achievementList;

        foreach (var progress in achievementProgressList)
        {
            var achievementBase = achievementList.Find(a => a.id == progress.progessID);
            var achievementUi = Instantiate(AchievementPrefabsUi, rootUiAchievement);
            achievementUi.SetAchievementData(achievementBase, progress);
        }
    }
    public void TakeAllReward()
    {
        var allDailyMisson = GetComponentsInChildren<QuestUi>(rootUiDailyMission);
        var allRepeatMisson = GetComponentsInChildren<QuestUi>(rootUiRepeatMisson);
        var allAchievement = GetComponentsInChildren<AchievementUi>(rootUiAchievement);

        foreach (var daily in allDailyMisson)
        {
            if (daily.missionProgess.isComplete && !daily.missionProgess.isTook)
            {
                allReward.rewardListToShow.Add(daily.missionBase.reward);
                daily.TakeReward();
            }
        }
        foreach (var repeat in allRepeatMisson)
        {
            if (repeat.missionProgess.isComplete && !repeat.missionProgess.isTook)
            {
                allReward.rewardListToShow.Add(repeat.missionBase.reward);
                repeat.TakeReward();
            }
        }
        foreach (var achievemnt in allAchievement)
        {
            if (achievemnt.achievementProgess.isComplete && !achievemnt.achievementProgess.isTook)
            {
                allReward.rewardListToShow.Add(achievemnt.achievementBase.reward);
                achievemnt.TakeReward();
            }
        }
        allReward.SetDataList();
    }
}
