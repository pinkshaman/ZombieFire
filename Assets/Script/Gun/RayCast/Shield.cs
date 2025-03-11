using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int hitCount = 0;
    private int maxHits = 5;

    public void TakeHit()
    {
        hitCount++;
        Debug.Log($"Shield count: {hitCount}");
        if (hitCount >= maxHits)
        {
            gameObject.SetActive(false);
        }
    }
}
