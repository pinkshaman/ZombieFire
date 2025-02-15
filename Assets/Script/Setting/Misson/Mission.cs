using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission : ScriptableObject
{
    public MissionType missionType;
    public List<MissionBase> missionList;
}
