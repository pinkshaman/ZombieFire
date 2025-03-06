using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    public Zombie zombie;
    public ZombieRepawn zombieRepawn;
    private bool isDeadByHeadShot;
    public BossHPBar healthBar;
    public override void Start()
    {
        Initialize();
        zombie = FindObjectOfType<Boss>();
        zombieRepawn = FindObjectOfType<ZombieRepawn>();
        OnHealthChange.AddListener(healthBar.Fill);
        OnTakeDamage.AddListener(zombie.OnGetHit);
    }
    public void Initialize()
    {
        maxHealthPoint = zombie.zombieData.Health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"zombieHP:{zombie.zombieData.Health}");
    }
    public override void Die()
    {
        zombieRepawn.OnZombieDeath(zombie.gameObject, gameObject);
        zombie.Die(isDeadByHeadShot);
        base.Die();
    }
    public void CheckHeadShot(RaycastHit hitInfo)
    {
        isDeadByHeadShot = hitInfo.collider.CompareTag("Head");
    }
}
