using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashShow : MonoBehaviour
{
    public RectTransform canvasRect;
    public float radius;
    public AutoFade Splash;
    public AudioSource audioSource;
    public void ShowRandomScratch()
    {
        Vector2 randomPosition = Random.insideUnitCircle * radius;
        RectTransform scratchRect = Splash.GetComponent<RectTransform>();
        scratchRect.anchoredPosition = randomPosition;
        Splash.Show();
        audioSource.Play();
    }

}
