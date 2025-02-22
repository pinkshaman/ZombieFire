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
    public UnityEvent<int> OnStartSpawn;
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
        OnStartSpawn.Invoke(quatity);
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
        int pointIndex = Random.Range(1, SpawnPotisionList.Count);
        Transform SpawnPoint = SpawnPotisionList[pointIndex].transform;
        Instantiate(zombiePrefabs, SpawnPoint.transform.position, SpawnPoint.transform.rotation);


    }
    public void OnzombieDeath()
    {
        Livezombie--;
        if (Livezombie == 0)
        {
            Debug.Log("Zombie Clear");
            OnZombieClear?.Invoke();
        }
    }

}
