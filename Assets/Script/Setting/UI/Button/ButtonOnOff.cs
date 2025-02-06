using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnOff : ButtonUi
{
    public GameObject Object;
    public bool activateOnClick;

    public override void OnClick()
    {
        if (Object != null)
        {
            Object.SetActive(!activateOnClick);
            base.OnClick();
        }
        else
        {
            Debug.LogWarning("Target object is not assigned", Object);
        }
    }
}
