using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AchievementBase 
{
    public int id;
    public string achievementName;
    public string achievementDescription;
    public  int achievementRequire;
    public string targetAchievement;
    public MissionRequireType achievementRequireType;
    public Reward reward;
}

[Serializable]
public class AchievementProgess
{
    public int progessID;
    public MissionRequireType achievementRequireType;
    public int currenProgess;
    public string progessTarget;
    public bool isComplete;
    public bool isTook;

    public AchievementProgess(int progessID,MissionRequireType achievementRequireType,int currentProgess, string progessTarget,bool isComplete, bool isTook)
    {
        this.progessID = progessID;
        this.achievementRequireType = achievementRequireType;
        this.currenProgess = currentProgess;
        this.progessTarget = progessTarget;
        this.isComplete = isComplete;
        this.isTook = isTook;
    }
}
[Serializable]
public class AchievementProgessList
{
    public List<AchievementProgess> achivementProgessList;
}