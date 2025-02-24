using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public LoadingGame LoadingPanel;

    public UnityAction<StageUi> chooseStage;

    public void Start()
    {
        chooseButton.onClick.AddListener(() => chooseStage.Invoke(this));
    }
    public void SetData(Stage data, StageProgess progess, UnityAction<StageUi> onClickCallback)
    {
        this.stageData = data;
        this.stageProgess = progess;
        UpdateUI(data, progess);
        this.chooseStage = onClickCallback;
    }
    public void UpdateUI(Stage stageData, StageProgess stageProgess)
    {
        stageName.text = $"STAGE {stageData.stageID}";
        stageImage.sprite = stageData.stageImage;
        lockObject.SetActive(!stageProgess.isCanPlay);
        bossLabel.SetActive(stageData.isBossWave);
        stageRank.gameObject.SetActive(stageProgess.isComplete);
        chooseButton.interactable = stageProgess.isCanPlay;
        if (stageProgess.isComplete)
        {
            var rank = stageData.rankList.rankLists.Find(rank => rank.rank == stageProgess.stageRank);
            if (rank != null)
            {
                stageRank.sprite = rank.rankSprite;
            }
        }
    }
}
