using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    public Zombie zombie;
    public ZombieRepawn zombieRepawn;

    public override void Start()
    {
        Initialize();
        zombieRepawn = FindObjectOfType<ZombieRepawn>();
    }
    public void Initialize()
    {
        zombie.zombieData = ZombieManager.Instance.GetZombieData(zombie.ZombieName);
        maxHealthPoint = zombie.zombieData.Health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"zombieHP:{zombie.zombieData.Health}");
    }
    public override void Die()
    {
        zombieRepawn.OnzombieDeath();
        base.Die();
    }
}
