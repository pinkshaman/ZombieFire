using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearUpgradeList", menuName = "GearUpgradeList/upgrade", order = 5)]
public class GearUpgradeList : ScriptableObject
{
    public List<GearUpgradeData> gearUpgradeLists;

}

