using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource explosionSound;
    public int lifeTime;
    public void ExplosionPlay()
    {
        explosionSound.Play();
        Destroy(gameObject, 3);
    }
    public void Start()
    {
       ExplosionPlay();
    }
}
