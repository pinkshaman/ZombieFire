using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieHealth : Health
{
    public Zombie zombie;
    public ZombieRepawn zombieRepawn;
    public bool isDeadByHeadShot;
    public HPBar healthBar;

    public override void Start()
    {
        Initialize();

        zombieRepawn = FindFirstObjectByType<ZombieRepawn>();
        OnHealthChange.AddListener(healthBar.Fill);
        OnTakeDamage.AddListener(zombie.OnGetHit);
        OnTakeDamage.AddListener(healthBar.FacingPlayer);
    }
    public virtual void Initialize()
    {
        maxHealthPoint = zombie.zombieData.Health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"zombieHP:{zombie.zombieData.Health}");
    }
    public override void Die()
    {
        zombieRepawn.OnZombieDeath(zombie.gameObject);
        zombie.Die(isDeadByHeadShot);
        MissonManager.Instance.UpdateMissionProgress(MissionRequireType.Kill,zombie.zombieData.ZombieName, 1);
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Kill,zombie.zombieData.ZombieName,1);
        MissonManager.Instance.UpdateAchievementProgess(MissionRequireType.Kill, "Zombie", 1);

       
        base.Die();
    }
    public void CheckHeadShot(RaycastHit hitInfo)
    {
        isDeadByHeadShot = hitInfo.collider.CompareTag("Head");
    }
}
