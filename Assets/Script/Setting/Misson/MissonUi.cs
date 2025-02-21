using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MissonUi : MonoBehaviour
{
    public Text missionName;
    public Image progessBar;
    public Image fillBar;
    public Text progessText;
    public Button getRewardButton;
    public GameObject uncompleteLabel;
    public Image rewardImage;
    public Text rewardAmout;
    public virtual void Start()
    {
        getRewardButton.onClick.AddListener(TakeReward);
    }
    public virtual void FillProgess(int progess, int baseRequire)
    {
        var fillPercent = progess / baseRequire;
        fillBar.fillAmount = fillPercent;
    }
   
    public abstract void TakeReward();

    public abstract void UpdateStatus();
  
}
