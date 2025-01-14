using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRepawn : MonoBehaviour
{
    public GameObject zombiePrefabs;
    public List<GameObject> SpawnPotisionList; 
    public int spawnQuatity;
    public float spawnSpeed;


    public void Start()
    {
        StartCoroutine(SpawnZombieByTime());
    }
    public IEnumerator SpawnZombieByTime()
    {
        while(spawnQuatity >0)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }
    private void SpawnZombie()
    {

        foreach (var point in SpawnPotisionList)
        {
            Transform SpawnPoint = point.transform;
            Instantiate(zombiePrefabs, SpawnPoint.transform.position, transform.rotation);
            spawnQuatity--;
        }
    }
}
