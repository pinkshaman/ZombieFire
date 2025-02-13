using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public PlayerData playerData;


    public override void Start()
    {
        playerData = PlayerManager.Instance.ReturnPlayerDataAfterEffected();
        maxHealthPoint = playerData.health;
        HealthPoint = maxHealthPoint;
        Debug.Log($"Player Health Effect:{playerData.health}");
    }

}
