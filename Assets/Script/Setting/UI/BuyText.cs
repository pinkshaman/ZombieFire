using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyText : MonoBehaviour
{
    public Text text;
    public float speed;
    public float visibleDuration;
    private float timer;

    private void OnEnable()
    {
        timer = visibleDuration;
    }
    private void OnDisable()
    {
        gameObject.transform.position = Vector3.zero;
    }

    public void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }

    }
    public void SetText(string value)
    {
        text.text = value;
        transform.position = gameObject.transform.position;
    }

}
