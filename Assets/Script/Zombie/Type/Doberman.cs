using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doberman : Zombie
{
    private float lastHit;
    public override void Attack()
    {
        base.Attack();
        FacingPlayer();
        if (Time.time - lastHit >= zombieData.duration)
        {
            AttackType();
            lastHit = Time.time;
        }
    }
    public void AttackType()
    {
        var attackIndex = Random.Range(0, 2);
        anim.SetInteger("AttackType",attackIndex);
    }

    public override void Move()
    {
        if (isDead || agent == null || !agent.isOnNavMesh) return;
        agent.isStopped = false;
        agent.SetDestination(playerTaget.position);
        anim.SetBool("isWalking", true);
    }

}
