using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissonUi : MonoBehaviour
{
    public Text missionName;
    public Image progessBar;
    public Image fillBar;
    public Text progessText;
    public Button getRewardButton;
    public GameObject uncompleteLabel;
    public Image rewardImage;
    public Text rewardAmout;
    private MissionBase missionBase;
    private MissionProgess missionProgess;

    public void Start()
    {
        getRewardButton.onClick.AddListener(TakeReward);
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
    public void FillProgess(int progess, int baseRequire)
    {
        var fillPercent = progess / baseRequire;
        fillBar.fillAmount = fillPercent;
    }
    public void CheckStatus(MissionProgess progess)
    {
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
    public void TakeReward()
    {
        PlayerManager.Instance.TakeReward(missionBase.reward);
        missionProgess.isTook = true;
        UpdateStatus();
    }
    public void UpdateStatus()
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
