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

    public void SetStageUi(Stage stageData, StageProgess stageProgess)
    {
        stageName.text = $"STAGE {stageData.stageID}";
        stageImage.sprite = stageData.stageImage;
        lockObject.SetActive(stageProgess.isComplete);
        bossLabel.SetActive(stageData.isBossWave);
        if (stageProgess.isComplete)
        {
            var rank = stageData.rankList.rankLists.Find(rank => rank.rank == stageProgess.stageRank);
            stageRank.sprite = rank.rankSprite;
        }
    }
}
