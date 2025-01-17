using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public float stageID;
    public Stage stage;
    public ZombieRepawn zombieRepawn;



    public void Start()
    {
        stage = GameManager.Instance.LoadStageData(stageID);
    }

    public void SpawnEnemy()
    {
        StartCoroutine(zombieRepawn.SpawnZombieByTime());
    }
}
