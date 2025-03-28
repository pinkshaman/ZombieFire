using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum AlertType
{
    Start,
    Wave,
    Clear,
    TimeUp,
    Kill,
    HeadShot,
    ButtonClick,

}
[Serializable]
public class AlertData 
{
    public AlertType alertType;
    public string textWarning;

    public AudioClip audio;
}
