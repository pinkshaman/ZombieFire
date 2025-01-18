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
        if (hitInfo.collider.CompareTag("Head"))
        {
            damage = Mathf.RoundToInt(damage * critical);
            healthTarget.TakeDamage(damage);
            damageTextPooling.ShowDamage(hitInfo.point, damage);
            if (healthTarget.IsDead)
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
        }
        else
        {
            healthTarget.TakeDamage(damage);
            damageTextPooling.ShowDamage(hitInfo.point, damage);
            if (healthTarget.IsDead)
            {
                killAlert.gameObject.SetActive(true);
                killAlert.KillAlertPlay();
                OnKill?.Invoke();
            }
        }
    }

    private IEnumerator ResetHeadShotCombo()
    {
        yield return new WaitForSeconds(4.0f);
        headShotCount = 0;
        comboHeadShot = null;
    }
}
