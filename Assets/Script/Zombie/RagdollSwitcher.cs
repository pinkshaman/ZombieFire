using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{
    public Rigidbody[] rigids;
    public Animator anim;

    [ContextMenu("Retrieve RigidBodies")]
    private void RetrieveRigidBodies()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
    }

    [ContextMenu("Clear Ragdoll")]
    private void ClearRagdoll()
    {
        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();
        foreach (CharacterJoint joint in joints)
        {
            DestroyImmediate(joint.gameObject);
        }
        Rigidbody[] rigidList = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigidList)
        {
            DestroyImmediate(rigid.gameObject);
        }
        Collider[] colls = GetComponentsInChildren<Collider>();
        foreach (Collider coll in colls)
        {
            DestroyImmediate(coll.gameObject);
        }
    }
    [ContextMenu("Enable Ragdoll")]
    public void EnableRagdoll()
    {
        SetRagdoll(true);
    }
    [ContextMenu("Disable Ragdoll")]
    public void DisableRagdoll()
    {
        SetRagdoll(false);
    }
    private void SetRagdoll(bool isEnable)
    {
        anim.enabled = !isEnable;
        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = !isEnable;
        }
    }
    [ContextMenu("Add HitSurface")]
    private void AddHitSurface()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            if (gameObject.GetComponent<HitSurface>() == null)
            {
                var hitSurface = collider.gameObject.AddComponent<HitSurface>();
                hitSurface.hitSurFaceType = HitSurfaceType.Blood;
            }
        }
    }
    [ContextMenu("DisableCollier")]
    public void DisableCollier()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
            if (collider.gameObject.CompareTag("Helmet") || collider.gameObject.CompareTag("Unbreakable"))
            {
                collider.enabled = true;
            }
        }
    }
}
