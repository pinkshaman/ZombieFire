using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetZombie : Zombie
{
    private float lastHit;
    public GameObject helmet;

    public override void Attack()
    {
        base.Attack();
        FacingPlayer();
        if (Time.time - lastHit >= zombieData.duration)
        {
            anim.SetTrigger("Attack");
            lastHit = Time.time;
        }
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Die(bool isHeadShot)
    {
        base.Die(isHeadShot);
    }
}
