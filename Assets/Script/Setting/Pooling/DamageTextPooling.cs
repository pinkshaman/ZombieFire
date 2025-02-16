using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageTextPooling : MonoBehaviour
{
    public DamageText damgageTextPrefabs;
    public int Poolsize = 50;
    private List<DamageText> damageTextPool;

    public void Start()
    {
        damageTextPool = new List<DamageText>();

        for (int i = 0; i < Poolsize; i++)
        {
            DamageText obj = Instantiate(damgageTextPrefabs, transform);
            obj.gameObject.SetActive(false);
            damageTextPool.Add(obj);
        }
    }

    private DamageText GetPoolObject()
    {
        foreach (DamageText obj in damageTextPool)
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        return null;
    }
    public void ShowDamage(Vector3 potision, int damage, bool isCritHit)
    {
        DamageText damageText = GetPoolObject();
        damageText.gameObject.SetActive(true);

        Color damageColor = isCritHit ? Color.yellow : Color.white;
        damageText.SetText(damage.ToString(), potision, damageColor);

    }
}
