using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI Instance { get; private set; }

    public Text fastRealoadQuatityText;
    public Text shieldQuatityText;
    public int fastRealoadCount;
    public int shieldCount;
    public Button buttonShield;
    public Button reloadButton;
    public ScratchShow scratchShow;
    public AutoFade ShieldEffect;
    public UnityEvent OnUsingShield;

    private bool _isUsingShield;
    public bool IsUsingShield
    {
        get => _isUsingShield;
        set
        {
            _isUsingShield = value;
            OnUsingShield.Invoke();
        }
    }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void Start()
    {
        SetData();
    }
    public void SetData()
    {
        fastRealoadCount = PlayerManager.Instance.ReturnNumberItem("QuickReload");
        shieldCount = PlayerManager.Instance.ReturnNumberItem("Shield");
        UpdateTextUi();
    }
    public void UpdateTextUi()
    {
        fastRealoadQuatityText.text = fastRealoadCount.ToString();
        shieldQuatityText.text = shieldCount.ToString();
    }
    public void UserReloadItem()
    {
        if (fastRealoadCount <= 0) return;
        fastRealoadCount--;
        PlayerManager.Instance.UpdateItemData("QuickReload", fastRealoadCount);
        UpdateTextUi();
    }
    public void UserShieldItem()
    {
        if (shieldCount <= 0) return;
        shieldCount--;
        buttonShield.interactable = false;
        IsUsingShield = true;
        PlayerManager.Instance.UpdateItemData("Shield", shieldCount);
        UpdateTextUi();
        StartCoroutine(DisableShieldAfterTime(5f));
    }
    private IEnumerator DisableShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsUsingShield = false;
        buttonShield.interactable = true;
    }
    public void ShowScratch()
    {
        scratchShow.ShowRandomScratch();
    }
    public void ShieldEffectShow()
    {
        ShieldEffect.fadeDuration = 1;
        ShieldEffect.visibleDurtation = 5;
        ShieldEffect.Show();
    }

}
