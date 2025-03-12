using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieRepawn : MonoBehaviour
{
    public List<GameObject> SpawnPositionList;
    public List<GameObject> BossSpawnPos;
    private int total;
    public float spawnSpeed;
    private int liveZombie;
    public GameObject HpBoss;
    public UnityEvent<int, int> OnZombieChange;
    public UnityEvent OnZombieClear;
    public UnityEvent OnSpawnDone;
    public UnityEvent<int> OnStartSpawn;
    private Dictionary<string, Queue<GameObject>> zombiePools = new Dictionary<string, Queue<GameObject>>();
    public int LiveZombie
    {
        get => liveZombie;
        set
        {
            liveZombie = value;
            OnZombieChange.Invoke(liveZombie, total);
        }
    }
    public void InitData(int quantity)
    {
        LiveZombie = quantity;
        total = quantity;
        OnStartSpawn.Invoke(quantity);
    }
    public void StartWave(BaseWave baseWave)
    {
        int totalZombies = 0;
        foreach (var group in baseWave.zombieList)
        {
            totalZombies += group.quatity;
        }

        InitData(totalZombies);
        StartCoroutine(SpawnWaveCoroutine(baseWave));
    }

    private IEnumerator SpawnWaveCoroutine(BaseWave baseWave)
    {

        foreach (var group in baseWave.zombieList)
        {
            for (int i = 0; i < group.quatity; i++)
            {
                SpawnZombie(group.zombie);
                yield return new WaitForSeconds(spawnSpeed);
            }
        }

        OnSpawnDone.Invoke();
    }
    public virtual void SpawnZombie(GameObject zombiePrefab)
    {
        if (zombiePrefab == null) return;

        Transform spawnPoint;

        if (zombiePrefab.GetComponent<Boss>() != null && BossSpawnPos.Count > 0)
        {
            spawnPoint = BossSpawnPos[Random.Range(0, BossSpawnPos.Count)].transform;
            HpBoss.SetActive(true);
        }
        else if (SpawnPositionList.Count > 0)
        {
            spawnPoint = SpawnPositionList[Random.Range(0, SpawnPositionList.Count)].transform;
        }
        else
        {
            return;
        }

        GameObject zombie = GetZombieFromPool(zombiePrefab);
        zombie.transform.position = spawnPoint.position;
        zombie.transform.rotation = spawnPoint.rotation;
        zombie.SetActive(true);
    }

    public GameObject GetZombieFromPool(GameObject zombiePrefab)
    {
        string prefabName = zombiePrefab.name;

        if (!zombiePools.ContainsKey(prefabName))
        {
            zombiePools[prefabName] = new Queue<GameObject>();
        }

        if (zombiePools[prefabName].Count > 0)
        {
            return zombiePools[prefabName].Dequeue();
        }
        else
        {
            return Instantiate(zombiePrefab);
        }
    }

    public void ReturnZombieToPool(GameObject zombie)
    {
        zombie.SetActive(false);
        zombie.GetComponent<Zombie>().ResetState();

        if (!zombiePools.ContainsKey(zombie.name))
        {
            zombiePools[zombie.name] = new Queue<GameObject>();
        }

        zombiePools[zombie.name].Enqueue(zombie);
    }

    public void OnZombieDeath(GameObject zombie)
    {
        LiveZombie--;
        StartCoroutine(ReturnZombieWithDelay(zombie, 3f));
        if (LiveZombie <= 0)
        {
            Debug.Log("Zombie Clear");
            OnZombieClear?.Invoke();
        }
    }

    private IEnumerator ReturnZombieWithDelay(GameObject zombie, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnZombieToPool(zombie);
    }

    private void ResetZombieState(GameObject zombie)
    {
        var zombieScript = zombie.GetComponent<Zombie>();
        zombieScript.ResetState();
    }
}
