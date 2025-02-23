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
    public override void Die(bool isHeadShot)
    {

        if (isDead) return;
        isDead = true;
        StopMove();
        Debug.Log("Zombie Dead");
        audioSource.clip = properties.clipDie;
        audioSource.Play();
        RageObj.SetActive(false);
        BoneRig.SetActive(false);

        anim.SetBool("isDead", true);
        Destroy(gameObject, 2.0f);

    }
}
