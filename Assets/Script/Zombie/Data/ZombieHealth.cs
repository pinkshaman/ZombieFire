using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ZombieHealth : Health
{
    public Zombie zombie;
    public ZombieRepawn zombieRepawn;
    private bool isDeadByHeadShot;
    public HPBar healthBar;
    public override void Start()
    {
        Initialize();
        
        var hpBar = GetComponentInChildren<HPBar>();
        healthBar =FindObjectOfType<HPBar>(hpBar);
        zombieRepawn = FindObjectOfType<ZombieRepawn>();
        OnHealthChange.AddListener(healthBar.Fill);
        OnTakeDamage.AddListener(zombie.OnGetHit);
        OnTakeDamage.AddListener(healthBar.FacingPlayer);
        OnTakeDamage.AddListener(healthBar.ShowText);
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
        zombie.Die(isDeadByHeadShot);
        base.Die();
    }
    public void CheckHeadShot(RaycastHit hitInfo)
    {
        isDeadByHeadShot = hitInfo.collider.CompareTag("Head");
    }
}
