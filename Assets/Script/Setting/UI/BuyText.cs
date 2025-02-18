using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyText : MonoBehaviour
{
    public Text text;
    public float moveSpeed = 50f; // Tốc độ chạy lên trên
    public float fadeDuration = 0.5f; // Thời gian mờ dần
    public float visibleDuration = 1.5f; // Thời gian hiển thị trước khi mờ
    private float timer;
    private Vector3 startPosition;
    private Color startColor;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        startPosition = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Tạo CanvasGroup nếu chưa có
        }
    }

    private void OnEnable()
    {
        timer = visibleDuration;
        transform.position = startPosition;
        canvasGroup.alpha = 1; // Hiện full opacity
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (timer > 0)
        {
            transform.position += moveSpeed * Time.deltaTime * Vector3.up; // Chạy lên trên
            timer -= Time.deltaTime;
            yield return null;
        }

        // Bắt đầu hiệu ứng mờ dần
        float fadeTimer = fadeDuration;
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            canvasGroup.alpha = fadeTimer / fadeDuration; // Giảm alpha dần
            yield return null;
        }

        // Reset về trạng thái ban đầu
        gameObject.SetActive(false);
        transform.position = startPosition;
    }

    public void SetText(string value)
    {
        text.text = $" -{value}";
    }

}
