using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerGun
{
    public string gunName;
    public bool isUnlocked;

}

[SerializeField]
public class PlayerGunList
{
    public List<PlayerGun> playerGuns;
}
