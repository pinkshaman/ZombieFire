using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPrefabs : MonoBehaviour
{
    public int lifeTime;

    public void Start()
    {
        Destroy(gameObject,lifeTime);
    }
}
