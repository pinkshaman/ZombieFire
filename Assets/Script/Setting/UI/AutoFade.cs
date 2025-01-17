using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFade : MonoBehaviour
{
    public float visibleDurtation;
    public float fadeDuration; 
    public CanvasGroup group;

    private float startTime;
    
 
    public void Start()
    {
        group.alpha = 0;
        gameObject.SetActive(false);
        
       
    }
    public void Show()
    {
        startTime = Time.time;
        group.alpha = 1f;
        gameObject.SetActive(true);
        

    }

    public void Hide()
    {
        group.alpha = 0;
        gameObject.SetActive(false);       
    }

    public void Update()
    {
        float elapsedTime = Time.time - startTime;

        if(elapsedTime < visibleDurtation) return;

    
        elapsedTime -= visibleDurtation;
        if( elapsedTime < fadeDuration)
        {
           group.alpha = 1- elapsedTime/fadeDuration;

        }   
        else
        {
            Hide();
        }
    }
}
