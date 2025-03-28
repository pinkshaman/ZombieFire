using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUi : MissonUi
{
    public MissionBase missionBase;
    public MissionProgess missionProgess;


    public override void Start()
    {
        getRewardButton.onClick.AddListener(TakeReward);
    }
    public void UpdateMissionProgess(MissionProgess missionProgess)
    {
        this.missionProgess = missionProgess;
        CheckStatus(missionProgess);
        SetMissionData(missionBase,missionProgess);
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
        if (missionProgess.isComplete&& missionProgess.isTook) return;
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
            getRewardButton.interactable = true;
            getRewardButton.image.color = Color.green;
            uncompleteLabel.SetActive(false);
        }
        else
        {
            getRewardButton.interactable = false;
            uncompleteLabel.SetActive(false);
            getRewardButton.image.color = Color.gray;
        }
        if(progess.isTook)
        {
            getRewardButton.interactable = false;
            getRewardButton.image.color = Color.gray;
            uncompleteLabel.SetActive(true);
        }
    }
    public override void TakeReward()
    {
        PlayerManager.Instance.TakeReward(missionBase.reward);
        missionProgess.isTook = true;
        UpdateStatus();
        CheckStatus(missionProgess);
        

    }
    public override void UpdateStatus()
    {
        if (missionProgess.missionType == MissionType.Repeat)
        {
            missionProgess.isTook = false;
            missionProgess.isComplete = false;
            missionProgess.missionProgessRequire = 0;
            UpdateMissionProgess(missionProgess);
            MissonManager.Instance.UpdateDataProgess(missionProgess);
        }
        else
        {
            missionProgess.isTook = true;
            MissonManager.Instance.UpdateDataProgess(missionProgess);
            UpdateMissionProgess(missionProgess);
        }
    }
}
