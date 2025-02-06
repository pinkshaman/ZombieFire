using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public ScratchShow scratchShow;
    public LevelUpdate levelUpdate;
    public Currency currency;
    public List<GunSlotUi> gunsSlotUi;

    public void Start()
    {
        PlayerManager.Instance.OnPlayerDataChange.AddListener(UpdateUi);

    }
    public void SetDataPlayer(PlayerData data)
    {
        UpdateUi(data);

    }
    public void UpdateUi(PlayerData data)
    {
        levelUpdate.SetExp(data);
        currency.LoadCurrencyData(data);
        UpdateSlotGun();
    }
    public void UpdateSlotGun()
    {
        var gunSlot = GunManager.Instance.gunSlot;
        List<string> gunNames = new List<string> { gunSlot.gunSlot1, gunSlot.gunSlot2 };
        for (int i = 0; i < gunsSlotUi.Count; i++)
        {
            BaseGun gunData = GunManager.Instance.GetGun(gunNames[i]);
            PlayerGun playerGun = GunManager.Instance.GetPlayerGun(gunNames[i]);

            if (gunData != null && playerGun != null)
            {
                gunsSlotUi[i].SetGunSlotUi(gunData, playerGun);
            }
        }
    }

    public void ShowScratch()
    {
        scratchShow.ShowRandomScratch();
    }


}

