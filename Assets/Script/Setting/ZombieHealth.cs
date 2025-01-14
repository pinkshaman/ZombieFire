using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    public Zombie zombie;
    public DamageText damageText;

    public override void Start()
    {
        maxHealthPoint = zombie.zombieData.Health;
        base.Start();
    }
    public override void TakeDamage(int damage)
    {
        if (IsDead) return;
        HealthPoint -= damage;
        damageText.ShowDamage(damage.ToString());
        OnTakeDamage.Invoke();
        if (IsDead)
        {
            Die();
        }
    }
}
