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
    public Reward reward;
}

[Serializable]
public class AchievementProgess
{
    public int progessID;
    public string progessTarget;
    public bool isComplete;
    public bool isTook;

    public AchievementProgess(int progessID, string progessTarget,bool isComplete, bool isTook)
    {
        this.progessID = progessID;
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