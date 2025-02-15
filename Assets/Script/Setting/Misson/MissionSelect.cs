using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelect : MonoBehaviour
{
    public Transform rootUiRepeatMisson;
    public Transform rootUiDailyMission;
    public Transform rootUiAchievement;
    public MissonUi UiPrefabs;
    public Button getAllRewardButton;

    public void Start()
    {
        CreateAllMission();
        getAllRewardButton.onClick.AddListener(TakeAllReward);
    }
    public void CreateAllMission()
    {
        var AllMission = MissonManager.Instance.missionProgessDictionary;
        foreach (var mission in AllMission)
        {
            var (missionType, missionID) = mission.Key;
            var (progress, missionBase) = mission.Value;

            var missionUiInstance = Instantiate(UiPrefabs);
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
    public void TakeAllReward()
    {
       
    }


}
