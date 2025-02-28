using System.Collections.Generic;
using UnityEngine;

public class AllReward : MonoBehaviour
{
    public List<Reward> rewardListToShow;
    public Transform rootUi;
    public RewardUi rewardPrefabs;
    public void SetDataList()
    {
        List<Reward> mergedList = new List<Reward>();

        foreach (var reward in rewardListToShow)
        {
            var existingReward = mergedList.Find(r => r.rewardType == reward.rewardType);
            if (existingReward != null)
            {
                existingReward.rewardAmmout += reward.rewardAmmout;
            }
            else
            {
                mergedList.Add(new Reward { rewardType = reward.rewardType, rewardAmmout = reward.rewardAmmout });
            }
        }

        rewardListToShow = mergedList;
    }
    public void ShowReward()
    {
        foreach (var reward in rewardListToShow)
        {
            var rewardToShow = Instantiate(rewardPrefabs, rootUi);
            rewardToShow.SetDataReward(reward);
            GetReward(reward);
        }
    }

    public void GetReward(Reward reward)
    {
        PlayerManager.Instance.TakeReward(reward);
    }
}
