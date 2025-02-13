using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealthPoint;
    protected int healthPoint;

    public UnityEvent onDie;
    public UnityEvent<int, int> OnHealthChange;
    public UnityEvent OnTakeDamage;

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = value;
            OnHealthChange.Invoke(healthPoint, maxHealthPoint);
        }
    }

    public bool IsDead => HealthPoint <= 0;

    public virtual void Start()
    {
        HealthPoint = maxHealthPoint;
    }
    public virtual void InitHealth(int maxHealth)
    {
        maxHealthPoint = maxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        if (IsDead) return;
        HealthPoint -= damage;
        OnTakeDamage.Invoke();
        if (IsDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        onDie.Invoke();
    }
}
