using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{

    public Zombie zombie;

    public override void Start()
    {
        maxHealthPoint = zombie.zombieData.Health;
        base.Start();
    }
}
