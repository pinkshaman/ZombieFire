using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissonRepeat", menuName = "Missions/Repeat", order = 1)]
public class MissonRepeat : Mission
{
    private void OnEnable()
    {
        missionType = MissionType.Repeat;
    }
}
