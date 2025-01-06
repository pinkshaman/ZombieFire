using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosinPrefabs;
    public float exposionRadius;
    public float exposionForce;
    public AudioClip exlosionSound;
    public int damage;
    public GameObject fireTail;

    public void Start()
    {
        fireTail.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.collider.gameObject.tag}");
        Instantiate(explosinPrefabs, transform.position, transform.rotation);
        PlaySound();
        Destroy(gameObject);
        BlowObject();
    }
    public void PlaySound()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound(exlosionSound);
    }
    private void BlowObject()
    {
        Collider[] effectedObject = Physics.OverlapSphere(transform.position, exposionRadius);
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
            rigidbody.AddExplosionForce(exposionForce, transform.position, exposionRadius, 1, ForceMode.Impulse);
        }
    }
}
