using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BonusZombie : Zombie
{
    public BonusZombieRepawn bonusZombieRepawn;

    public float stopDistance;

    public override void Awake()
    {
        StartCoroutine(InitializeZombieData());
        bonusZombieRepawn = FindFirstObjectByType<BonusZombieRepawn>();
        SetDestination();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stopDistance = 0.5f;
    }
    
    public void SetDestination()
    {
        var randomEndPod = Random.Range(0, bonusZombieRepawn.EndPos.Count);
        playerTaget = bonusZombieRepawn.EndPos[randomEndPod].transform;
    }

    public void CheckArrivedAtTarget()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position,playerTaget.position);
        if (distance <= stopDistance)
        {
            OnReachEndPos();
        }
    }
    public void OnReachEndPos()
    {
        ZombieRepawn respawnManager = FindFirstObjectByType<ZombieRepawn>();
        if (respawnManager != null)
        {
            respawnManager.ReturnZombieToPool(gameObject);
        }
    }

    public override void Update()
    {
        if (isDead) return;
        CheckDistance();
        CheckOffMesh();
        CheckHeight();
        CheckArrivedAtTarget();
        if (IsMoving)
        {
            Move();
        }
    }
}
