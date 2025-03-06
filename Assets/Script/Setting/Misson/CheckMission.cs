using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission : MonoBehaviour
{

    public GameObject statusImage;


    public void Start()
    {
        CheckStatus();
    }
    public void CheckStatus()
    {
        var achievementProgressList = MissonManager.Instance.achievementProgessList.achivementProgessList;
        var missionProgessList = MissonManager.Instance.missionProgessList.missionProgessList;

        bool hasReward = false;

        foreach (var achievement in achievementProgressList)
        {
            if (achievement.isComplete && !achievement.isTook)
            {
                hasReward = true;
                break;
            }
        }

        foreach (var mission in missionProgessList)
        {
            if (mission.isComplete && !mission.isTook)
            {
                hasReward = true;
                break;
            }
        }

        statusImage.SetActive(hasReward);
    }
}
