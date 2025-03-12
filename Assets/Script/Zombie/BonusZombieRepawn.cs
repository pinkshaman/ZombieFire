using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZombieRepawn : ZombieRepawn
{
    public List<GameObject> EndPos;



    public override void SpawnZombie(GameObject zombiePrefab)
    {
        base.SpawnZombie(zombiePrefab);

        Zombie zombieScript = zombiePrefab.GetComponent<Zombie>();
        if (zombieScript != null && EndPos.Count > 0)
        {
            Vector3 randomEndPos = EndPos[Random.Range(0, EndPos.Count)].transform.position;
            zombieScript.SetTargetPosition(randomEndPos);
        }
    }
}
