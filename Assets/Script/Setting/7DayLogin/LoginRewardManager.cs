using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LoginRewardProgess
{
    public int day;
    public bool isTook;
    public bool isCanTake;
}
[Serializable]
public class LoginRewardProgessList
{
    public List<LoginRewardProgess> loginRewardProgessList;
}
public class LoginRewardManager : MonoBehaviour
{
    public List<LoginReward> loginRewardList;
    public LoginRewardProgessList loginRewardProgessLists;
    private DateTime lastClaimDate;
    public GameObject rewardLoginPanel;
    public void Start()
    {
        Load7dayReward();
        CheckNewDay();
        SetData();
        rewardLoginPanel.SetActive(true);
    }

    [ContextMenu("InitLoginProgessList")]
    public void InitLoginProgessList()
    {
        loginRewardProgessLists = new LoginRewardProgessList();
        loginRewardProgessLists.loginRewardProgessList = new List<LoginRewardProgess>();
        for (int i = 1; i <= 7; i++)
        {
            loginRewardProgessLists.loginRewardProgessList.Add(new LoginRewardProgess { day = i, isTook = false, isCanTake = (i == 1) });
            
        }
        Save7dayReward();
    }
    public void SetData()
    {
        foreach (var loginReward in loginRewardProgessLists.loginRewardProgessList)
        {
            var reward = loginRewardList.Find(r => r.day == loginReward.day);
            reward.SetData(loginReward.isTook, loginReward.isCanTake);

        }
    }
 
    private void CheckNewDay()
    {
        string lastClaimStr = PlayerPrefs.GetString("LastClaimDate", "");
        if (!string.IsNullOrEmpty(lastClaimStr))
        {
            lastClaimDate = DateTime.Parse(lastClaimStr);
        }
        else
        {
            lastClaimDate = DateTime.Now.Date.AddDays(-1);
        }

        if (DateTime.Now.Date > lastClaimDate.Date)
        {
            UnlockNextReward();
            lastClaimDate = DateTime.Now.Date;
            PlayerPrefs.SetString("LastClaimDate", lastClaimDate.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
    }
    private void UnlockNextReward()
    {
        foreach (var reward in loginRewardProgessLists.loginRewardProgessList)
        {
            if (!reward.isTook)
            {
                reward.isCanTake = true;
                Save7dayReward();
                return;
            }
        }
    }
    public void ClaimReward(int day)
    {
        var reward = loginRewardProgessLists.loginRewardProgessList.Find(r => r.day == day);
        if (reward != null && reward.isCanTake && !reward.isTook)
        {
            reward.isTook = true;
            
            Save7dayReward();
        }
    }

    public void Save7dayReward()
    {
        var value = JsonUtility.ToJson(loginRewardProgessLists);
        PlayerPrefs.SetString(nameof(loginRewardProgessLists), value);
        PlayerPrefs.Save();
    }
    public void Load7dayReward()
    {
        var defaultValue = JsonUtility.ToJson(loginRewardProgessLists);
        var json = PlayerPrefs.GetString(nameof(loginRewardProgessLists), defaultValue);
        loginRewardProgessLists = JsonUtility.FromJson<LoginRewardProgessList>(json);
        Debug.Log("Reward7day is Loaded");
    }
}
