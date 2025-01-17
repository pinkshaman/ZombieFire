using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public TextMesh text;
    public float speed;
    public float visibleDuration;
    private float timer;
    private float baseSpeed;
    private void OnEnable()
    {
        timer = visibleDuration;
    }

    public void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);     
        timer-= Time.deltaTime;
        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }

    }
    public void SetText(string value, Vector3 potision)
    {
        text.text = value;
        transform.position = potision;
    }
   


}
