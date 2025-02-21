using System;
using System.Collections;
using Unity.VisualScripting;



public enum MissionType
{
    Daily,
    Repeat,
}


[Serializable]
public class MissionBase 
{
    public int missionID;
    public MissionRequireType missionRequireType;
    public string missionName;
    public string missionDecription;
    public int missionRequire;
    public string tagertName;
    public Reward reward;
}
public enum MissionRequireType
{
    Kill,
    Collect,
    Play,
    Buy,
    Get,
}
