using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public GamePlayUI itemUI;
    public bool _isShieldActive = false;

    public PlayerHpBar playerHpBar;
    public GameOver gameOver;

    public override void Start()
    {
        itemUI = FindFirstObjectByType<GamePlayUI>();
        maxHealthPoint = PlayerManager.Instance.ReturnPlayerHealthAfterEffected();
        HealthPoint = maxHealthPoint;
        itemUI.OnUsingShield.AddListener(UpdateShieldState);
        OnHealthChange.AddListener(playerHpBar.Fill);
        Debug.Log($"Player Health Effect:{maxHealthPoint}");
        gameOver.ContinueButton.onClick.AddListener(OnRespawn);
    }
    private void UpdateShieldState()
    {
        _isShieldActive = itemUI.IsUsingShield;
        Debug.Log($"Shield Status: {_isShieldActive}");
    }
    public override void TakeDamage(int damage)
    {
        if (IsDead) return;
        if (_isShieldActive)
        {
            Debug.Log("Shield is Actived, doesn't take damage");
            return;
        }
        HealthPoint -= damage;
        OnTakeDamage.Invoke();
        if (IsDead)
        {
            StartCoroutine(ShowGameOver());
            Die();
        }
    }
    public IEnumerator ShowGameOver()
    {
        gameOver.Show();
        yield return new WaitForSecondsRealtime(gameOver.anim.clip.length);
        Time.timeScale = 0;
        gameOver.EnableButtons();
    }

    public void OnRespawn()
    {
        HealthPoint = maxHealthPoint;
        
    }
}
