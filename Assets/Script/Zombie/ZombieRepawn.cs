using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieRepawn : MonoBehaviour
{
    public Stage stage;
    public List<GameObject> SpawnPotisionList;
    public int spawnTotal;
    private int total;
    public float spawnSpeed;
    private int liveZombie;
    public UnityEvent<int, int> OnZombieChange;
    public int Livezombie
    {
        get => liveZombie;
        set
        {
            liveZombie = value;
            OnZombieChange.Invoke(liveZombie, spawnTotal);
        }
    }

    public void Start()
    {
        Livezombie = spawnTotal;
        total = spawnTotal;
        StartCoroutine(SpawnZombieByTime());
    }
    public IEnumerator SpawnZombieByTime()
    {
        while (total > 0)
        {
            //SpawnZombie();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }
    private void SpawnZombie(GameObject zombiePrefabs)
    {
        foreach (var point in SpawnPotisionList)
        {
            Transform SpawnPoint = point.transform;
            Instantiate(zombiePrefabs, SpawnPoint.transform.position, transform.rotation);
            total--;
        }
    }
    public void OnzombieDeath()
    {
        Livezombie--;
    }

}
