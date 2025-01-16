using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Alert", menuName = "AlertList/Alert", order = 4)]

public class AlertDataList : ScriptableObject
{
    public List<AlertData> alertDataList;
}
