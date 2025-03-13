using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasedPanel : MonoBehaviour
{
    public Text ammount;

    public void SetData(int coin)
    {
        ammount.text = coin.ToString();
    }

}
