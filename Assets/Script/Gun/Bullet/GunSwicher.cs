using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunSwicher : MonoBehaviour
{
    public GameObject[] gunListed;
    public AudioSource switchingSound;
    public Button buttonSwitch;
    private int gunIndex1, gunIndex2;
    private bool isSwitching = false;
    private RotateByMouse cameraRotation;
    public void Start()
    {
        cameraRotation = FindObjectOfType<RotateByMouse>();
        InitializeGuns();
        buttonSwitch.onClick.AddListener(Switch);
    }
    private void InitializeGuns()
    {
        var gunSlot = GunManager.Instance.ReturnGunSlot();
        for (int i = 0; i < gunListed.Length; i++)
        {
            var gun = gunListed[i].GetComponent<Gun>();
            if (gun.GunName == gunSlot.gunSlot1) gunIndex1 = i;
            if (gun.GunName == gunSlot.gunSlot2) gunIndex2 = i;
        }
        gunListed[gunIndex1].SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isSwitching)
        {
            Switch();
        }
    }
    public void Switch()
    {
        StartCoroutine(SwitchGun());
    }
    private IEnumerator SwitchGun()
    {
        isSwitching = true;

        int currentGunIndex = gunListed[gunIndex1].activeSelf ? gunIndex1 : gunIndex2;
        int newGunIndex = (currentGunIndex == gunIndex1) ? gunIndex2 : gunIndex1;

        Gun currentGun = gunListed[currentGunIndex].GetComponent<Gun>();
        Gun newGun = gunListed[newGunIndex].GetComponent<Gun>();

        currentGun.Hiding();
        currentGun.Switching();

        var timetoHide = currentGun.ReturnHideTime();
        yield return new WaitForSeconds(timetoHide);


        newGun.transform.rotation = cameraRotation.verticalPivot.rotation;


        gunListed[currentGunIndex].SetActive(false);
        gunListed[newGunIndex].SetActive(true);
        switchingSound.Play();

        newGun.Switching();
        newGun.Ready();

        isSwitching = false;
    }
}


