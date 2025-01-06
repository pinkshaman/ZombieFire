using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealthPoint;
    private int healthPoint;

    public UnityEvent onDie;
    public UnityEvent<int, int> OnHealthChange;
    public UnityEvent OnTakaDamage;

    public int HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = value;
            OnHealthChange.Invoke(healthPoint,maxHealthPoint);
        }
    }

    private bool IsDead => HealthPoint <= 0;

    private void Start()
    {
        HealthPoint = maxHealthPoint;
    }
    public void TakeDamage(int damage)
    {
        if (!IsDead) return;
        HealthPoint-=damage;
        OnTakaDamage.Invoke();
        if(IsDead)
        {
            Die();
        }
    }
    private void Die() => onDie.Invoke();   
}
