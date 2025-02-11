using System;
using UnityEngine;


[Serializable]
public  class Rifle 
{  
    public GunModel BaseModel;
    public GunAudio gunAudio;
    public DrawRay ray;
    public GunRayCaster gunRayCaster;
    public ParticleSystem shellBullet;
   

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