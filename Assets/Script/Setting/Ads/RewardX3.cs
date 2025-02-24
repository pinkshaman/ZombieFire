using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardX3 : RewardedAdsButton
{
    public Result result;
    public override void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (!_hasRewarded)
            {
                _hasRewarded = true;
                Debug.Log("x3 reward by Watched Ads!");
                result.WatchAdsButton();              
            }
            
        }
    }
}
