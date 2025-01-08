using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunMenu", menuName = "Gun/BaseGun")]
[Serializable]
public class GunList : ScriptableObject
{  
    public List<BaseGun> baseGunList;
}



