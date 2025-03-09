using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllReward : MonoBehaviour
{
    public List<Reward> rewardListToShow;
    public Transform rootUi;
    public RewardUi rewardPrefabs;
    public void SetDataList()
    {
        gameObject.SetActive(true);
        List<Reward> mergedList = new List<Reward>();

        foreach (var reward in rewardListToShow)
        {
            var existingReward = mergedList.Find(r => r.rewardType == reward.rewardType);
            if (existingReward != null)
            {
                existingReward.rewardAmmout += reward.rewardAmmout;
                existingReward.rewardImage = reward.rewardImage;
            }
            else
            {
                mergedList.Add(new Reward { rewardType = reward.rewardType, rewardAmmout = reward.rewardAmmout,rewardImage = reward.rewardImage });
            }
        }

        rewardListToShow = mergedList;
        ShowReward();
    }
    public void ShowReward()
    {
        if (rewardListToShow == null || rewardListToShow.Count == 0) return;
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
