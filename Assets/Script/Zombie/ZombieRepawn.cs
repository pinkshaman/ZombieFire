using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieRepawn : MonoBehaviour
{
    public List<GameObject> SpawnPotisionList;
    private int total;
    public float spawnSpeed;
    private int liveZombie;
    public UnityEvent<int, int> OnZombieChange;
    public UnityEvent OnZombieClear;
    public UnityEvent OnSpawnDone;
    public int Livezombie
    {
        get => liveZombie;
        set
        {
            liveZombie = value;
            OnZombieChange.Invoke(liveZombie, total);
        }
    }
    public void InitData(int quatity)
    {
        Livezombie = quatity;
        total = quatity;
    }
    public IEnumerator SpawnZombieByTime(GameObject zombie, int quatity)
    {
        InitData(quatity);
        while (quatity > 0)
        {
            SpawnZombie(zombie);
            quatity--;
            yield return new WaitForSeconds(spawnSpeed);
            if (quatity == 0)
            {
                OnSpawnDone.Invoke();
            }
        }
    }
    private void SpawnZombie(GameObject zombiePrefabs)
    {
        foreach (var point in SpawnPotisionList)
        {
            Transform SpawnPoint = point.transform;
            Instantiate(zombiePrefabs, SpawnPoint.transform.position, transform.rotation);       
        }
    }
    public void OnzombieDeath()
    {
        Livezombie--;
        if (Livezombie == 0)
        {
            OnZombieClear?.Invoke();
        }
    }

}
