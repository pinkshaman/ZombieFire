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
    public RectTransform rectObject;
    private void OnEnable()
    {
        timer = visibleDuration;
    }
    private void OnDisable()
    {
        gameObject.transform.position = rectObject.position;
    }

    public void Update()
    {
        transform.position = speed * Time.deltaTime * Vector2.up;
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
