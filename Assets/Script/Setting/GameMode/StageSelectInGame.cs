using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectInGame : StageSelect
{

    public override void LoadStage(StageUi stageUI)
    {
        Debug.Log($"Data :{stageUI.stageData.sceneID}");
        StageGameMode.Instance.SetCurrentArenaAndStage(stageUI.stageData.stageID);
        MySceneManager.Instance.LoadSceneByID(stageUI.stageData.sceneID);
    }
}
