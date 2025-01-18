using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieCount : MonoBehaviour
{
    public Text valueCount;
    public ZombieRepawn zombieRepawn;
    public void Start()
    {
        valueCount.text = $"{zombieRepawn.Livezombie}/ {zombieRepawn.Livezombie}";
        zombieRepawn.OnStartSpawn.AddListener(SetCount);
        zombieRepawn.OnZombieChange.AddListener(ShowText);
    }
    public void ShowText(int currentZombie, int totalZombie)
    {
        valueCount.text = $"{currentZombie}/ {totalZombie}";
    }
    public void SetCount(int count)
    {
        valueCount.text = $"{count}/ {count}";
    }
}
