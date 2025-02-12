using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwicher : MonoBehaviour
{
    public GameObject[] GunList;
    public AudioSource swichingSound;
    private string gunSlot1;
    private string gunSlot2;

    public void Start()
    {
        SetActiveGun();

    }
    public void SetActiveGun()
    {
        var gunSlot = GunManager.Instance.ReturnGunSlot();
        gunSlot1 = gunSlot.gunSlot1;
        gunSlot2 = gunSlot.gunSlot2;
    }
    private void Update()
    {
        for (int i = 0; i < GunList.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SetAvtiveGun(i);
            }
        }
    }
    private void SetAvtiveGun(int gunIndex)
    {

        for (int i = 0; i < GunList.Length; i++)
        {
            var gun =GunList[i].GetComponent<Gun>();
            bool isActive = (i == gunIndex);
            if (gun.GunName == gunSlot1 || gun.GunName == gunSlot2)
            {
                GunList[i].SetActive(isActive);
                if (isActive)
                {
                    swichingSound.Play();
                    GunList[i].SendMessage("OnGunSelected", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }   
}
