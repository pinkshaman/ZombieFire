using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionForce;
    public int damage;
    public float bulletSpeed;
    public GameObject fireTail;
    public Rigidbody rb;
    public bool isShooting;

    public void ShootRocket(Vector3 startPosition, Quaternion rotation)
    {
        transform.position = startPosition;
        transform.rotation = rotation;
        isShooting = true;
        gameObject.SetActive(true);
    }

    public void Update()
    {
        if (!isShooting) return;
        transform.position += bulletSpeed * Time.deltaTime * transform.forward;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            return;
        }
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
        isShooting = false;
        gameObject.SetActive(false);
        RocketPooling.Instance.ReturnRocket(gameObject);
    }
}
