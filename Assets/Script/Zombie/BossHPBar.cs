using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : HPBar
{
    public GameObject zombieCount;

    public override void Start()
    {
        zombieCount.SetActive(false);
    }
    public void ActiveZombieCount()
    {
        zombieCount.SetActive(true);
    }
}
