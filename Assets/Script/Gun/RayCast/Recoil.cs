using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Camera aimingCamera;
    public float recoilRecoverySpeed = 5f;
    public float horizontalRecoil = 1f;
    public float verticalRecoil = 1f;
    public float recoilAmount = 2f;

    private Vector3 currentRecoil = Vector3.zero;
    private Vector3 targetRecoil = Vector3.zero;

    public Ray CalculateRecoilRay()
    {
        Vector3 recoilOffset = new Vector3(
            Random.Range(-horizontalRecoil, horizontalRecoil),
            Random.Range(-verticalRecoil, verticalRecoil),
            0
        );

        return new Ray(aimingCamera.transform.position,aimingCamera.transform.forward + aimingCamera.transform.TransformDirection(recoilOffset));
    }

    public void ApplyRecoil()
    {
        targetRecoil += new Vector3(-verticalRecoil, Random.Range(-horizontalRecoil, horizontalRecoil), 0) * recoilAmount;
    }

    private void Update()
    {
        currentRecoil = Vector3.Lerp(currentRecoil, Vector3.zero, Time.deltaTime * recoilRecoverySpeed);
    }
}
