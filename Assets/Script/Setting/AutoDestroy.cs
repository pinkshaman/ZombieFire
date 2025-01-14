using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public GameObject Object;

    public void Start()
    {
        Destroy(Object, 4.5f);
    }

}
