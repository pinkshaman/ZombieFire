using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUi : MonoBehaviour
{
    public Text rewardAmmout;
    public Image rewardImage;


    public void SetDataReward(Reward reward)
    {
        rewardAmmout.text = reward.rewardAmmout.ToString();
        rewardImage.sprite = reward.rewardImage;
    }
}
