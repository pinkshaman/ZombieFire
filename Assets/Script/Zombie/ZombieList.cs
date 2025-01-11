using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ZombieList", menuName = "ZombieList/Zombie", order = 1)]

public class ZombieList : ScriptableObject
{
  public List<BaseZombie> zombieList;
}
