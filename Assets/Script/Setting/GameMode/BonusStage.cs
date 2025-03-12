using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BonusStage", menuName = "ArenaList/BonusStage")]

public class BonusStage : ScriptableObject
{
    public List<BaseWave> bonusWaves;
}
