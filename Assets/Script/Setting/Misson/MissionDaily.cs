using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionDaily", menuName = "Missions/Daily", order = 2)]
public class MissionDaily : Mission
{
    private void OnEnable()
    {
        missionType = MissionType.Daily;
    }
}
