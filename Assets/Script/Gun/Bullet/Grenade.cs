using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject ExplosionPrefabs;
    public int lifeTime = 3;
    public float explosionRadius;
    public float explosionForce;
    public AudioSource eplosionSound;
    public int damage;

    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.collider.gameObject.tag}");
        Instantiate(ExplosionPrefabs, transform.position, transform.rotation);
        PlaySound();
        Destroy(gameObject);
        BlowObject();
    }
    public void PlaySound()
    {
        eplosionSound.Play();
    }
    private void BlowObject()
    {
        Collider[] effectedObject = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < effectedObject.Length; i++)
        {
            DeliverDamage(effectedObject[i]);
            AddForceToObject(effectedObject[i]);
        }
    }
    private void DeliverDamage(Collider victim)
    {
        Health health = victim.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);

        }
    }
    private void AddForceToObject(Collider effectedObject)
    {
        Rigidbody rigidbody = effectedObject.attachedRigidbody;
        if (rigidbody)
        {
            DeliverDamage(effectedObject);
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1, ForceMode.Impulse);
        }
    }

}
