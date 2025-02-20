
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType
{
    Normal,
    Helmet,
    Shield,
    DoberMan,
    Bat,
    Boss,


}

[Serializable]
public class ZombieProperties
{
    public ZombieType Type;
    public GameObject Prefabs;
    public AudioClip clipDie;
    public AudioClip clipDeadbyHS;
    public AudioClip clipShot;
    public AudioClip clipRage;
    public AudioClip clipAttack;
}



[Serializable]
public class BaseZombie 
{
    public string ZombieName;
    public int Damage;
    public float RangedAtk;
    public int Health;
    public float Speed;
    public float duration;
    

}
