using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MissionProgess 
{
    public int missionProgessID;
    public MissionType missionType;
    public MissionRequireType missinRequireType;
    public bool isComplete;
    public bool isTook;
    public int missionProgessRequire;
    public string targetName;

    public MissionProgess(int missionProgessID,MissionType missionType, MissionRequireType missinRequireType, bool isComplete, int missionProgessRequire, string targetName)
    {
        this.missionProgessID = missionProgessID;
        this.missionType = missionType;
        this.missinRequireType = missinRequireType;
        this.isComplete = isComplete;
        this.missionProgessRequire = missionProgessRequire;
        this.targetName = targetName;
    }
}

[Serializable]
public class MissonProgessList
{
    public List<MissionProgess> missionProgessList;
}
