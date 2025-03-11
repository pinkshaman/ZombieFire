using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillSplat : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;
    public int damage;
    public void SetTarget(Vector3 targetPosition, int Damage)
    {
        target = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        damage = Damage;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GamePlayUI.Instance.ShowSplash();
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Explode();
        }
    }
}
