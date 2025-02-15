using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpdate : MonoBehaviour
{
    public Text levelText;
    public Text expPercentText;

    private int playerLevel = 1;
    private float playerExp = 0f;
    private float nextLevelExp = 100f;
    private float expIncreaseRate = 1.3f;

    public PlayerLevelUpPanel playerLevelUpPanel;
    private bool isWaitingForReward = false;

    private PlayerData playerData;

    public void SetExp(PlayerData data)
    {
        this.playerData = data;
        playerLevel = 0;
        nextLevelExp = 100f;
        playerExp = data.exp;
        StartCoroutine(ProcessLevelUp());
    }

    public void AddExp(float amount)
    {
        playerExp += amount;
        StartCoroutine(ProcessLevelUp());
    }

    private IEnumerator ProcessLevelUp()
    {
        if (isWaitingForReward) yield break;

        while (playerExp >= nextLevelExp)
        {
            playerExp -= nextLevelExp;
            playerLevel++;
            nextLevelExp *= expIncreaseRate;
            UpdateUI();
            if (CheckReward(playerLevel))
            {
                yield return new WaitUntil(() => !isWaitingForReward);
            }
        }
    }

    private void UpdateUI()
    {
        float expPercent = (playerExp / nextLevelExp) * 100f;
        levelText.text = $"{playerLevel}";
        expPercentText.text = $"{expPercent:F2}%";
    }

    private bool CheckReward(int level)
    {
        foreach (var reward in playerData.levelRewardProgessList.leveRewardProgesses)
        {
            if (reward.levelProgess == level && !reward.isTook)
            {
                isWaitingForReward = true;
                playerLevelUpPanel.SetData(PlayerManager.Instance.ReturnLevelRewardBase(level));
                playerLevelUpPanel.gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void TakeReward(LevelRewardBase rewardBase)
    {
        if (rewardBase.reward.rewardType == RewardType.Coin)
        {
            playerData.coin += rewardBase.reward.rewardAmmout;
        }
        else if (rewardBase.reward.rewardType == RewardType.Gold)
        {
            playerData.gold += rewardBase.reward.rewardAmmout;
        }

        foreach (var reward in playerData.levelRewardProgessList.leveRewardProgesses)
        {
            if (reward.levelProgess == rewardBase.level)
            {
                reward.isTook = true;
                break;
            }
        }

        PlayerManager.Instance.UpdatePlayerData(playerData);
        isWaitingForReward = false;
        playerLevelUpPanel.gameObject.SetActive(false);
        StartCoroutine(ProcessLevelUp());
    }
}
