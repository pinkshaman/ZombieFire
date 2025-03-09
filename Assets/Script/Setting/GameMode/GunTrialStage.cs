using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunTrialStage", menuName = "ArenaList/GunTrialStage")]

public class GunTrialStage : ScriptableObject
{
    public List<BaseWave> waveList;
}
