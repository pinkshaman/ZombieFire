using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : ZombieHealth
{

    public override void Start()
    {
        Initialize();
        zombieRepawn = FindObjectOfType<ZombieRepawn>();
        healthBar =FindObjectOfType<BossHPBar>();
        OnHealthChange.AddListener(healthBar.Fill);
        OnTakeDamage.AddListener(zombie.OnGetHit);
    }
    public override void Initialize()
    {
        maxHealthPoint = zombie.zombieData.Health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"zombieHP:{zombie.zombieData.Health}");
    }
    public override void Die()
    {
        zombieRepawn.OnZombieDeath(zombie.gameObject, gameObject);
        zombie.Die(isDeadByHeadShot);
        MissonManager.Instance.UpdateMissionProgress(MissionRequireType.Kill, zombie.zombieData.ZombieName, 1);
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Kill, zombie.zombieData.ZombieName, 1);
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Kill, "Zombie", 1);
    }
}
