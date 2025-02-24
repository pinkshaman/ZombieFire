using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text stageNumber;
    public Text rankText;
    public Text killText;
    public Text headShotText;
    public Text maxHeadShotComboText;
    public Text scoreText;
    public Text expText;
    public Text coinReward;

    public Button mainMenuButton;
    public Button selectStageButton;
    public Button reTryButton;
    public Button nextStageButton;
    public Button watchAdsAndx3Reward;

    public DamageManagement damageManagement;
    public Score score;
    public GameObject chooseStage;
    public void Start()
    {
        SetData();
        mainMenuButton.onClick.AddListener(MainMenuButtonAction);
        selectStageButton.onClick.AddListener(SelectStageButton);
        reTryButton.onClick.AddListener(RetryButton);
        nextStageButton.onClick.AddListener(NextStageButton);

    }
    public void SetData()
    {
        stageNumber.text = $"Stage {StageGameMode.Instance.ReturnCurrentStageforPlay().stageID.ToString()}";
        maxHeadShotComboText.text = damageManagement.ReturnMaxHeadShotKill().ToString();
        killText.text = score.ReturnKillCount();
        headShotText.text = score.ReturnHeadShotCount();
        rankText.text = score.ReturnRank();
        maxHeadShotComboText.text = damageManagement.ReturnMaxHeadShotKill();
        scoreText.text = score.ReturnScore();
        expText.text = score.ReturnExp();
        int coin = score.ReturnRewardCoin();
        int reward = RewardCoinCalculator(coin, rankText.text);
        coinReward.text = reward.ToString();
        UpdateReward();
    }
    public int RewardCoinCalculator(int coin, string currenRank)
    {
        if (currenRank == "S")
        {
            return (coin * (1 + 100 / 100));
        }
        else if (currenRank == "A")
        {
            return (coin * (1 + 80 / 100));
        }
        else if (currenRank == "B")
        {
            return (coin * (1 + 50 / 100));
        }
        else if (currenRank == "C")
        {
            return (coin * (1 + 20 / 100));
        }
        else
        {
            return coin;
        }
    }

    public void MainMenuButtonAction()
    {
        MySceneManager.Instance.LoadSceneByID(0);
    }
    public void SelectStageButton()
    {
        chooseStage.SetActive(true);
    }
    public void RetryButton()
    {
        var stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
        MySceneManager.Instance.LoadSceneByStageID(stage);
    }
    public void NextStageButton()
    {
        if (StageGameMode.Instance.currentStageLoad > 19)
        {
            StageGameMode.Instance.currentArenaLoad += 1;
            StageGameMode.Instance.currentStageLoad = 1;
            var stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
            MySceneManager.Instance.LoadSceneByStageID(stage);
        }
        else
        {
            StageGameMode.Instance.currentStageLoad++;
            var stage = StageGameMode.Instance.ReturnCurrentStageforPlay();
            MySceneManager.Instance.LoadSceneByStageID(stage);
        }
    }
    public void WatchAdsButton()
    {
        var playerData = PlayerManager.Instance.playerData;
        int coin = int.Parse(coinReward.text);
        playerData.coin += (2 * coin);
        int exp = int.Parse(expText.text);
        playerData.exp += (2 * exp);
        PlayerManager.Instance.UpdatePlayerData(playerData);
    }
    public string ReturnRank()
    {
        return rankText.text;
    }
    public void UpdateReward()
    {
        var playerData = PlayerManager.Instance.playerData;

        int coin = int.Parse(coinReward.text);
        playerData.coin += coin;
        int exp = int.Parse(expText.text);
        playerData.exp += exp;

        PlayerManager.Instance.UpdatePlayerData(playerData);
    }

}
