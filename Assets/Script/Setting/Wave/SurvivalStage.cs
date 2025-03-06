using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SurvivalStage", menuName = "ArenaList/SurvivalStage")]

public class SurvivalStage: ScriptableObject
{
    public int coinReward;
    public int time;
    public List<BaseWave> waveList;
}

