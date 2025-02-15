using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUpPanel : MonoBehaviour
{
    public Text level;
    public Text rewardQuatity;
    public Image rewardType;
    public Button takeRewardButton;
    public LevelUpdate levelUpdate;

    private LevelRewardBase currentReward;
    public void Start()
    {
        takeRewardButton.onClick.AddListener(TakeReward);
    }
    public void SetData(LevelRewardBase levelRewardBase)
    {
        currentReward = levelRewardBase;
        level.text = levelRewardBase.level.ToString();
        rewardQuatity.text = levelRewardBase.reward.rewardAmmout.ToString();
        rewardType.sprite = levelRewardBase.reward.rewardImage;
        rewardType.SetNativeSize();
    }
    private void TakeReward()
    {
        if (currentReward != null)
        {
            levelUpdate.TakeReward(currentReward);
        }
    }
}
