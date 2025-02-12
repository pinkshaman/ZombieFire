using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public AlertDataList alertDataList;
    public static AlertManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public AlertData InitAlertData(AlertType type)
    {
        var alertData = alertDataList.alertDataList.Find(alertData => alertData.alertType == type);
        return alertData;
    }


    
}
