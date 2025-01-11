using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType
{
    Berserk,
    Varial,
    DoberMan,

}

[Serializable]
public class ZombieProperties
{
    public ZombieType Type;
    public GameObject Prefabs;
    public AudioClip clipDie;
    public AudioClip clipDeadbyHS;
    public AudioClip clipGetHit;
    public AudioClip clipRage;
}



[Serializable]
public class BaseZombie 
{
    public string ZombieName;
    public int Damage;
    public float RangedAtk;
    public float Health;


}
