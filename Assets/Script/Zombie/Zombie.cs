using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    public BaseZombie zombieData;
    public ZombieProperties properties;
    public Animator anim;
    public AudioSource audioSource;


    public virtual void Start()
    {
        
    }
    public virtual void Attack()
    {

    }
    public virtual void Die()
    {

    }
    public virtual void Move()
    {

    }
}
