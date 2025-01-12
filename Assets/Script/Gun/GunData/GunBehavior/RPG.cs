using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RPG 
{
    public GunModel BaseModel;
    public GameObject bulletPrefabs;
    public GunAudio gunAudio;
    public Transform firingPos;
    public GameObject rocket; 
    public ParticleSystem muzzleSmoke;


    public float GetAnimationLength(Animator animator, string animationName)
    {
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator does not have a RuntimeAnimatorController!");
            return 0f;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                Debug.Log($"ReloadClip:{clip.length}");
                return clip.length;
            }
        }
        Debug.LogWarning($"Animation with name '{animationName}' not found!");
        return 0f;
    }
}
