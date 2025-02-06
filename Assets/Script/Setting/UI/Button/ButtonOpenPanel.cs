using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenPanel : ButtonUi
{
    public GameObject Panel;

    public override void OnClick()
    {
        base.OnClick();
        Panel.SetActive(true);
    }

}
