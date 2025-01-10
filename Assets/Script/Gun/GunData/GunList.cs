using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunMenu", menuName = "Gun/BaseGun",order =1)]
[Serializable]
public class GunList : ScriptableObject
{  
    public List<BaseGun> baseGunList;
}



