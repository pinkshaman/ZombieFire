using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwicher : MonoBehaviour
{
    public GameObject[] GunList;
    public AudioSource swichingSound;
    
    private void Update()
    {
        for (int i = 0; i < GunList.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetAvtiveGun(i);
            }
        }
    }
    private void SetAvtiveGun(int gunIndex)
    {
        for (int i = 0; i < GunList.Length; i++)
        {
            bool isActive = (i == gunIndex);
            GunList[i].SetActive(isActive);
            if (isActive)
            {
                swichingSound.Play();
                GunList[i].SendMessage("OnGunSelected", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
