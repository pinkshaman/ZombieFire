using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionForce;
    public int damage;
    public GameObject fireTail;
    public Rigidbody rb;


    public void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        BlowObjects();
        ResetRocket();
    }

    public void BlowObjects()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var obj in affectedObjects)
        {
            Rigidbody rb = obj.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1, ForceMode.Impulse);
            }

            Health health = obj.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    public void ResetRocket()
    {
        RocketPooling.Instance.ReturnRocket(gameObject);
    }
}
