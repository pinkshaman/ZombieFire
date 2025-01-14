using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText;


    public void ShowDamage(string text)
    {
        damageText.text = text;
        Debug.Log($"ShowDamage:{text}");
    }
}
