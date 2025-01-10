using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[Serializable]
public  class Rifle 
{  
    public GunModel BaseModel;
    public GunAudio gunAudio;
    public DrawRay ray;
    public GunRayCaster gunRayCaster;
    public ParticleSystem shellBullet;
    public Animator anim;
    public GunAmmo gunAmmo;
}