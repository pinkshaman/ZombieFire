using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementList", menuName = "AchievementList/Achievement", order = 9)]

public class AchievementList: ScriptableObject
{
    public List<AchievementBase> achievementList;
}
