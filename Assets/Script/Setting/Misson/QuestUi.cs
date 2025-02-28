using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUi : MissonUi
{
    public MissionBase missionBase;
    public MissionProgess missionProgess;


    public void UpdateMissionProgess(MissionProgess missionProgess)
    {
        this.missionProgess = missionProgess;
        CheckStatus(missionProgess);
    }
    public void SetMissionData(MissionBase missionBase, MissionProgess missionProgess)
    {
        this.missionBase = missionBase;
        this.missionProgess = missionProgess;
        missionName.text = $"{missionBase.missionRequireType} {missionBase.missionRequire} {missionBase.tagertName}";
        progessText.text = $"{missionProgess.missionProgessRequire}/{missionBase.missionRequire}";
        rewardImage.sprite = missionBase.reward.rewardImage;
        rewardAmout.text = missionBase.reward.rewardAmmout.ToString();

        FillProgess(missionProgess.missionProgessRequire, missionBase.missionRequire);
        CheckStatus(missionProgess);
    }
    public override void FillProgess(int progess, int baseRequire)
    {
        var fillPercent = progess / baseRequire;
        fillBar.fillAmount = fillPercent;
    }
    public void CheckStatus(MissionProgess progess)
    {
        if (progess.missionProgessRequire >= missionBase.missionRequire)
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
        PlayerManager.Instance.TakeReward(missionBase.reward);
        missionProgess.isTook = true;
        UpdateStatus();
    }
    public override void UpdateStatus()
    {
        if (missionProgess.missionType == MissionType.Repeat)
        {
            missionProgess.isTook = false;
            missionProgess.isComplete = false;
            missionProgess.missionProgessRequire = 0;

            MissonManager.Instance.UpdateDataProgess(missionProgess);
        }
        else
        {
            MissonManager.Instance.UpdateDataProgess(missionProgess);
        }
    }
}
