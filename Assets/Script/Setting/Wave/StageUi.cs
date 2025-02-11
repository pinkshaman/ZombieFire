using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUi : MonoBehaviour
{
    public Text stageName;
    public Image stageRank;
    public GameObject lockObject;
    public Image stageImage;
    public GameObject bossLabel;
    public Stage stageData;
    public StageProgess stageProgess;
    public Button chooseButton;
    
    public void Start()
    {
        chooseButton.onClick.AddListener(LoadStage);
    }
    public void SetData(Stage data, StageProgess progess)
    {
        this.stageData = data;
        this.stageProgess = progess;
        UpdateUI(data,progess);
    }
    public void UpdateUI(Stage stageData, StageProgess stageProgess)
    {
        stageName.text = $"STAGE {stageData.stageID}";
        stageImage.sprite = stageData.stageImage;
        lockObject.SetActive(!stageProgess.isComplete);
        bossLabel.SetActive(stageData.isBossWave);
        stageRank.gameObject.SetActive(stageProgess.isComplete);
        if (stageProgess.isCanPlay )
        {
            lockObject.SetActive(!stageProgess.isCanPlay);
        }
        if (stageProgess.isComplete)
        {
            var rank = stageData.rankList.rankLists.Find(rank => rank.rank == stageProgess.stageRank);
            stageRank.sprite = rank.rankSprite;
        }
    }
    public void LoadStage()
    {

    }
}
