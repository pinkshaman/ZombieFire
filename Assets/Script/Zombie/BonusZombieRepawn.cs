using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZombieRepawn : ZombieRepawn
{
    public List<GameObject> EndPos;

    public override void SpawnZombie(GameObject zombiePrefab)
    {
        Transform spawnPoint;
        spawnPoint = SpawnPositionList[Random.Range(0, SpawnPositionList.Count)].transform;

        GameObject zombie = GetZombieFromPool(zombiePrefab);
        zombie.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        zombie.SetActive(true);

    }

    public List<GameObject> ReturnList()
    {
        return EndPos;
    }
}
