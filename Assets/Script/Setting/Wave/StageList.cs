using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageList", menuName = "StageList/Stage", order = 3)]


public class StageList : ScriptableObject
{
    public List<Stage> stageLists;

}