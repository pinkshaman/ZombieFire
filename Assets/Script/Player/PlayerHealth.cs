using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GamePlayUI itemUI;
    private bool _isShieldActive = false;



    public override void Start()
    {
        maxHealthPoint = PlayerManager.Instance.ReturnPlayerHealthAfterEffected();
        HealthPoint = maxHealthPoint;
        itemUI.OnUsingShield.AddListener(UpdateShieldState);
        Debug.Log($"Player Health Effect:{maxHealthPoint}");
    }
    private void UpdateShieldState()
    {
        _isShieldActive = itemUI.IsUsingShield;
        Debug.Log($"Shield Status: {_isShieldActive}");
    }
    public override void TakeDamage(int damage)
    {
        if (IsDead) return;
        if (_isShieldActive)
        {
            Debug.Log("Shield is Actived, doesn't take damage");
            return;
        }
        HealthPoint -= damage;
        OnTakeDamage.Invoke();
        if (IsDead)
        {
            Die();
        }

    }

}
