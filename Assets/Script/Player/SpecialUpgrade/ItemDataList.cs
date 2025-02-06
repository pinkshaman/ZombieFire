using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "ItemDataList/item", order = 6)]
public class ItemDataList : ScriptableObject
{
    public List<ItemData> itemDataLists;
}
