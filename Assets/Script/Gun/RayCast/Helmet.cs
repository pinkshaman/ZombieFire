using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 3;

    public void TakeHit()
    {
        hitCount++;
        Debug.Log($"Helmet hit count :{hitCount}");
        if (hitCount >= maxHits)
        {
            gameObject.SetActive(false);
        }
    }

}
