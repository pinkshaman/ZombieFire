using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public enum HitSurfaceType
{
    Dirt = 0,
    Blood = 1,
    ShieldHit = 2,
}
[Serializable]
public class HitEffectMapper
{
    public HitSurfaceType surfaceType;
    public GameObject hitEffect;
}
public class HitEffectManager : MonoBehaviour
{
    public List<HitEffectMapper> effectMap;
    public static HitEffectManager Instance { get; private set; }

    public void Awake()
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

    public GameObject GetEffectPrefabs(HitSurfaceType surFaceType)
    {
        var mapper = effectMap.Find(effect => effect.surfaceType == surFaceType);
        return mapper.hitEffect;
    }
}
