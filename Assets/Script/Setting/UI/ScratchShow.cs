using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchShow : MonoBehaviour
{
    public RectTransform canvasRect;
    public float radius;
    public AutoFade Scratch;
    public AutoFade redScreenEffect;
    public void ShowRandomScratch()
    {
        Vector2 randomPosition = Random.insideUnitCircle * radius;
        RectTransform scratchRect = Scratch.GetComponent<RectTransform>();
        scratchRect.anchoredPosition = randomPosition;
        Scratch.Show();
        redScreenEffect.Show();
    }
}
