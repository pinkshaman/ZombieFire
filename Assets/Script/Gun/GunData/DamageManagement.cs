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
    public void Calculator(RaycastHit hitInfo, int damage, Health healthTarget, float critical)
    {
        bool isHeadShot = hitInfo.collider.CompareTag("Head");
        if (isHeadShot)
        {
            damage = Mathf.RoundToInt(damage * (1 + critical / 100f));
        }

        healthTarget.TakeDamage(damage);
        damageTextPooling.ShowDamage(hitInfo.point, damage);

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
    private void HandleHeadShotKill()
    {
        headShotCount++;
        headShotAlert.gameObject.SetActive(true);
        headShotAlert.HeadShotAlertCount(headShotCount);
        OnHeadShot?.Invoke();

        if (comboHeadShot != null)
        {
            StopCoroutine(comboHeadShot);
        }
        comboHeadShot = StartCoroutine(ResetHeadShotCombo());
    }

    private void HandleNormalKill()
    {
        killAlert.gameObject.SetActive(true);
        killAlert.KillAlertPlay();
        OnKill?.Invoke();
    }
    private IEnumerator ResetHeadShotCombo()
    {
        yield return new WaitForSeconds(4.0f);
        headShotCount = 0;
        comboHeadShot = null;
    }
}
