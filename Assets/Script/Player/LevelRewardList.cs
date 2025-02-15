using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelReawardList", menuName = "LevelReawardList/LevelReward", order = 7)]

public class LevelRewardList :ScriptableObject
{
    public List<LevelRewardBase> listLevelReward;
}
