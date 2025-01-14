using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }
    public ZombieList zombies;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public BaseZombie GetZombieData(string name)
    {
        var zombieData = zombies.zombieList.Find(gun => gun.ZombieName == name);
        return zombieData;

    }
}
