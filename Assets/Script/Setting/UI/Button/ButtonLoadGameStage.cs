using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadGameStage : ButtonUi
{

    public override void Start()
    {
        base.Start();
    }
    public void LoadArena()
    {
        StageGameMode.Instance.StartGameMode();
    }
    public override void OnClick()
    {
        base.OnClick();
        LoadArena();
    }
}
