using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "AlertData", menuName = "AlertDataList/Alert", order = 4)]

public class AlertDataList : ScriptableObject
{
    public List<AlertData> alertDataList;
}
