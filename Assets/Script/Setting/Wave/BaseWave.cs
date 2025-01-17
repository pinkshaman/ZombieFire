using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseWave 
{
    public float waveNumber;
    public List<GroupZombie> zombieList;

}
[Serializable]
public class GroupZombie
{
    public GameObject zombie;
    public int quatity;

}
[Serializable]
public class Stage 
{
    public float stageID;
    public List<BaseWave> waveList;
}
