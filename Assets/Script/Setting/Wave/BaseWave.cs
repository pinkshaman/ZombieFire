using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseWave 
{
    public int waveNumber;
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
    public Sprite stageImage;
    public bool isBossWave;
    public StageRankList rankList;
    public List<BaseWave> waveList;
}
[Serializable]
public class StageRank
{
    public string rank;
    public Sprite rankSprite;
}
[Serializable]
public class StageRankList
{
    public List<StageRank> rankLists;
}

