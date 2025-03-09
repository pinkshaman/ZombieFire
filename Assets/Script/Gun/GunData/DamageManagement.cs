using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageManagement : MonoBehaviour
{
    public KillAlert killAlert;
    public HeadShotAlert headShotAlert;
    private int headShotCount;
    public DamageTextPooling damageTextPooling;
    private Coroutine comboHeadShot;
    public UnityEvent OnHeadShot;
    public UnityEvent OnKill;

    private float critticalDamageEffect;
    private float headShotEffect;
    private float damageIncease;
    private int maxHeadshotKill;
    private float totalCritical;
    public void Start()
    {
        critticalDamageEffect = PlayerManager.Instance.ReturnGearEffect(GearUpgradeType.Trouser);
        headShotEffect = PlayerManager.Instance.ReturnGearEffect(GearUpgradeType.Hat);
        damageIncease = PlayerManager.Instance.ReturnGearEffect(GearUpgradeType.Bag);
        maxHeadshotKill = 0;
    }
  
    public void Calculator(RaycastHit hitInfo, int damage, Health healthTarget, float critical)
    {
        totalCritical = critical + critticalDamageEffect;
        var newDamage = Mathf.RoundToInt(damage * (1 + damageIncease / 100f));
        damage = newDamage;
        bool isCriticalShot = Random.value < (totalCritical / 100f);
        if (isCriticalShot)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
        }

        bool isHeadShot = hitInfo.collider.CompareTag("Head");
        if (isHeadShot)
        {
            var newHeadShotDamage = Mathf.RoundToInt(damage * (1 + headShotEffect / 100f));
            damage = newHeadShotDamage;
        }

        healthTarget.TakeDamage(damage);
        damageTextPooling.ShowDamage(hitInfo.point, damage,isCriticalShot);

        if (healthTarget.IsDead)
        {
            if (isHeadShot)
            {
                HandleHeadShotKill();
            }
            else
            {
                HandleNormalKill();
            }
        }
    }
    public void HandleHeadShotKill()
    {
        headShotCount++;
        if(headShotCount > maxHeadshotKill)
        {
            maxHeadshotKill = headShotCount;
        }
        headShotAlert.gameObject.SetActive(true);
        headShotAlert.HeadShotAlertCount(headShotCount);
        OnHeadShot.Invoke();
        OnKill.Invoke();
        if (comboHeadShot != null)
        {
            StopCoroutine(comboHeadShot);
        }
        comboHeadShot = StartCoroutine(ResetHeadShotCombo());
    }

    public void HandleNormalKill()
    {
        killAlert.gameObject.SetActive(true);
        killAlert.KillAlertPlay();
        OnKill.Invoke();
    }
    private IEnumerator ResetHeadShotCombo()
    {
        yield return new WaitForSeconds(4.0f);
        headShotCount = 0;
        comboHeadShot = null;
    }
    public string ReturnMaxHeadShotKill()
    {
        int value = maxHeadshotKill;
        return value.ToString();
    }
}
