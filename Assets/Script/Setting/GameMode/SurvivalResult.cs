using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalResult : MonoBehaviour
{
    public Text killText;
    public Text headShotText;
    public Text maxHeadShotComboText;
    public Text waveArrivalText;
    public Text scoreText;
    public Text rewardText;

    public Button adsButton;
    public GameObject newRecordObject;
    public Button TouchToExit;

    public DamageManagement damageManagement;
    public Score score;
    public SurvivalGameFlow survivalGameFlow;
    private int currentScore;
    private int currentWave;

    public void Start()
    {
        var data = SurvivalMode.Instance.suvivalModeProgess;
        currentScore = data.highestScore;
        currentWave = data.highestWave;
        ShowResult();
        adsButton.onClick.AddListener(WatchAdsButton);

    }
    public void ShowResult()
    {
        maxHeadShotComboText.text = damageManagement.ReturnMaxHeadShotKill().ToString();
        killText.text = score.ReturnKillCount();
        headShotText.text = score.ReturnHeadShotCount();
        waveArrivalText.text = survivalGameFlow.ReturnWave().ToString();
        maxHeadShotComboText.text = damageManagement.ReturnMaxHeadShotKill();
        scoreText.text = score.ReturnScore();
        int coin = score.ReturnRewardCoin();
        rewardText.text = coin.ToString();
        UpdateReward();
        int.TryParse(scoreText.text, out int newScore);
        int.TryParse(waveArrivalText.text, out int newWave);
        if (newScore > currentScore || newWave > currentWave) 
        {
            newRecordObject.SetActive(true);
        }
    }
    public void WatchAdsButton()
    {
        var playerData = PlayerManager.Instance.playerData;

        int.TryParse(rewardText.text, out int coin);
        Reward newReward = new Reward();
        newReward.rewardType = RewardType.Coin;
        newReward.rewardAmmout = coin * 2;
        PlayerManager.Instance.TakeReward(newReward);
    }
    public void UpdateReward()
    {
        int.TryParse(rewardText.text, out int coin);
        Reward newReward = new Reward();
        newReward.rewardType = RewardType.Coin;
        newReward.rewardAmmout = coin;
        PlayerManager.Instance.TakeReward(newReward);
    }
}
