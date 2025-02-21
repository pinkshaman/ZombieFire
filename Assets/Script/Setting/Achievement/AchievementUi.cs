using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUi : MissonUi
{
    private AchievementBase achievementBase;
    private AchievementProgess achievementProgess;


    public void UpdateAchievement(AchievementProgess achievementProgess)
    {
        this.achievementProgess = achievementProgess;
        CheckAchievement(achievementProgess);
    }
    public void SetAchievementData(AchievementBase achievementBase, AchievementProgess achievementProgess)
    {
        this.achievementBase = achievementBase;
        this.achievementProgess = achievementProgess;
        missionName.text = $"{achievementBase.achievementRequireType} {achievementBase.achievementRequire} {achievementBase.targetAchievement}";
        progessText.text = $"{achievementProgess.currenProgess}/{achievementBase.achievementRequire}";
        rewardImage.sprite = achievementBase.reward.rewardImage;
        rewardAmout.text = achievementBase.reward.rewardAmmout.ToString();
        FillProgess(achievementProgess.currenProgess, achievementBase.achievementRequire);
        CheckAchievement(achievementProgess);
    }
    public void CheckAchievement(AchievementProgess progess)
    {
        if(progess.currenProgess >= achievementBase.achievementRequire)
        {
            progess.isComplete = true;
        }
        if (progess.isComplete)
        {
            getRewardButton.interactable = false;
            uncompleteLabel.SetActive(false);
        }
        else
        {
            getRewardButton.interactable = true;
            uncompleteLabel.SetActive(true);
        }
    }
    public override void TakeReward()
    {
        PlayerManager.Instance.TakeReward(achievementBase.reward);
        achievementProgess.isTook = true;
        UpdateStatus();
    }
    public override void UpdateStatus()
    {
       MissonManager.Instance.UpdateAchievementProgessData(achievementProgess);

    }
}
