using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviorList :MonoBehaviour
{
    public Rifle rifle;
    public Pistol pistol;
    public RPG rpg;
    public SMG smg;
    public Sniper sniper;
    public Shotgun shotgun;


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
                return clip.length;
            }
        }
        Debug.LogWarning($"Animation with name '{animationName}' not found!");
        return 0f;
    }
}
