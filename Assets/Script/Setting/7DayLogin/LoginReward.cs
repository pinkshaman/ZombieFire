using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginReward : MonoBehaviour
{
    public int day;
    public Text dayText;
    public Reward reward;
    public Image rewardImage;
    public Text quality;
    public GameObject checkObject;
    public GameObject recievedGameObject;
    public Button takeButton;
    public bool isTook;
    public bool isCanTake;

    public void Start()
    {
        takeButton.onClick.AddListener(TakeReward);
    }
    public void SetData(bool isTook, bool isCanTake)
    {
        this.isTook = isTook;
        this.isCanTake = isCanTake;
        dayText.text = day.ToString();
        rewardImage.sprite = reward.rewardImage;
        quality.text = $"X {reward.rewardAmmout}";
        checkObject.SetActive(isTook);
        recievedGameObject.SetActive(!isCanTake);
    }
    public void TakeReward()
    {
        if (isCanTake && !isTook)
        {
            isTook = true;
            PlayerManager.Instance.TakeReward(reward);
            LoginRewardManager rewardManager = FindObjectOfType<LoginRewardManager>();
            rewardManager.ClaimReward(day);
            UpdateUi();
        }
    }
    public void UpdateUi()
    {
        checkObject.SetActive(isTook);
        recievedGameObject.SetActive(!isCanTake);
    }
}
