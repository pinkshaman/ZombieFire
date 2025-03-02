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

        zombieRepawn = FindObjectOfType<ZombieRepawn>();
        OnHealthChange.AddListener(healthBar.Fill);
        OnTakeDamage.AddListener(zombie.OnGetHit);
        OnTakeDamage.AddListener(healthBar.FacingPlayer);
    }
    public void Initialize()
    {
        maxHealthPoint = zombie.zombieData.Health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"zombieHP:{zombie.zombieData.Health}");
    }
    public override void Die()
    {
        zombieRepawn.OnzombieDeath();
        zombie.Die(isDeadByHeadShot);
        MissonManager.Instance.UpdateMissionProgress(MissionRequireType.Kill,zombie.zombieData.ZombieName, 1);
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Kill,zombie.zombieData.ZombieName,1);

       
        base.Die();
    }
    public void CheckHeadShot(RaycastHit hitInfo)
    {
        isDeadByHeadShot = hitInfo.collider.CompareTag("Head");
    }
}
