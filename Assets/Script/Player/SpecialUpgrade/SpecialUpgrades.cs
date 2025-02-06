using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUpgrades: MonoBehaviour
{
    public SpecialType specialType;
    public Animation anim;
    public GameObject checkMark;

    public void PlayAnim()
    {
        anim.Play();
    }
}
public enum SpecialType
{
    Gear,
    Item,
}
