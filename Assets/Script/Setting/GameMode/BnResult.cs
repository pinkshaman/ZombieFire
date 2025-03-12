using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BnResult : Result
{
    public BonusScore bonusScore;
    public Text killText;
    public Text coinText;
    public Button adsButton;
    public Button touchToExitButton;
    public void ShowResult()
    {
        killText.text = bonusScore.ReturnKill().ToString();
        coinText.text = bonusScore.ReturnCoin().ToString();

    }

    public void Start()
    {
        ShowResult();
        adsButton.onClick.AddListener(WatchAdsButton);
        touchToExitButton.onClick.AddListener(TouchToExit);
    }
    public void TouchToExit()
    {
        SceneManager.LoadScene(0);
    }
    public override void WatchAdsButton()
    {
        var playerData = PlayerManager.Instance.playerData;

        int.TryParse(coinText.text, out int coin);
        Reward newReward = new Reward();
        newReward.rewardType = RewardType.Coin;
        newReward.rewardAmmout = coin * 2;
        PlayerManager.Instance.TakeReward(newReward);
    }
    public void UpdateReward()
    {
        int.TryParse(coinText.text, out int coin);
        Reward newReward = new Reward();
        newReward.rewardType = RewardType.Coin;
        newReward.rewardAmmout = coin;
        PlayerManager.Instance.TakeReward(newReward);
    }

}
