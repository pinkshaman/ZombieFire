using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchShow : MonoBehaviour
{
    public RectTransform canvasRect;
    public float radius;
    public AutoFade Scratch;
    public GameObject redScreenEffect;
    public void ShowRandomScratch()
    {
        Vector2 randomPosition = Random.insideUnitCircle * radius;
        RectTransform scratchRect = Scratch.GetComponent<RectTransform>();
        scratchRect.anchoredPosition = randomPosition;
        Scratch.Show();
        var clip = redScreenEffect.GetComponent<Animation>();
        redScreenEffect.gameObject.SetActive(true);
        clip.Play();
    }
}
