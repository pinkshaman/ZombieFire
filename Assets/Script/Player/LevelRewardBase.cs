using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class LevelRewardBase 
{
    public int level;
    public Reward reward;
}
public enum RewardType
{
    Gold,
    Coin,
    Exp,
    Permisson,
    Item,
}
[Serializable]
public class Reward
{
    public RewardType rewardType;
    public int rewardAmmout;
    public Sprite rewardImage;
    public string rewardName;
}

