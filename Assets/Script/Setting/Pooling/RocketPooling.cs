using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RocketPooling : MonoBehaviour
{
    public static RocketPooling Instance { get; private set; }
    public GameObject rocketPrefab;
    public int poolSize = 10;

    private Queue<GameObject> rocketPool = new Queue<GameObject>();

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

   public void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject rocket = Instantiate(rocketPrefab);
            rocket.SetActive(false);
            rocketPool.Enqueue(rocket);
        }
    }

    public GameObject GetRocket()
    {
        if (rocketPool.Count > 0)
        {
            GameObject rocket = rocketPool.Dequeue();
            rocket.SetActive(true);
            return rocket;
        }
        else
        {
            GameObject newRocket = Instantiate(rocketPrefab);
            return newRocket;
        }
    }

    public void ReturnRocket(GameObject rocket)
    {
        rocket.SetActive(false);
        rocketPool.Enqueue(rocket);
    }
}
