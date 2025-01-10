using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public  class Pistol 
{
    public GunModel BaseModel;
    public GunAudio gunAudio;
    public DrawRay ray;
    public GunRayCaster gunRayCaster;
    public ParticleSystem shellBullet;
    public Animator anim;
    public GunAmmo gunAmmo;
}
