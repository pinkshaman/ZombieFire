using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataBase", menuName = "PlayerDataBase/data", order = 7)]

public class PlayerDataBase : ScriptableObject
{
    public int level;
    public float exp;
    public int health;
    public int coin;
    public int gold;

    public PlayerDataBase(int level, float exp, int health, int coin, int gold )
    {
        this.level = level;
        this.exp = exp;
        this.health = health;
        this.coin = coin;
        this.gold = gold;
    }
}
