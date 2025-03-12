using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Zombie
{
    public GameObject splatSkill;
    public Transform firePos;
    public int skillSpeed;
    public List<Transform> SummonPos;
    public List<Zombie> zombieList;
    private bool isSummoned;
    public ParticleSystem smoke;
    public override void Start()
    {
        OnReachingRadius.AddListener(Attack);
        OnStartMoving.AddListener(StopAttack);
        agent.speed = zombieData.Speed;
        isRage = false;
        isGetHit = false;
        isDead = false;
        RageObj.SetActive(false);
        BoneRig.SetActive(true);
        var zombiehealth = GetComponentInParent<BossHealth>();
        zombiehealth.OnTakeDamage.AddListener(CheckGetHit);
        isSummoned = false;
    }
    public override void Die(bool isHeadShot)
    {
        if (isDead) return;
        isDead = true;
        StopMove();
        Debug.Log("Zombie Dead");
        audioSource.clip = properties.clipDie;
        audioSource.Play();
        RageObj.SetActive(false);
        BoneRig.SetActive(false);

        anim.SetBool("isDead", true);
    }
    public override void OnGetHit()
    {
        base.OnGetHit();
        if (isSummoned) return;
        isSummoned = true;
        anim.SetTrigger("Rise");
        smoke.Play();
        foreach (Transform pos in SummonPos)
        {
            int radomZombie = Random.Range(0, zombieList.Count);
            Instantiate(zombieList[radomZombie].gameObject, pos.position, Quaternion.identity);
        }

    }
    public override void Move()
    {
        if (isDead || agent == null || !agent.isOnNavMesh || isGetHit) return;
        agent.isStopped = false;
        agent.SetDestination(playerTaget.position);
        if (isRage)
        {
            int moveType = 2;
            anim.SetInteger("MoveType", moveType);
        }
        else
        {
            int moveType = 1;
            anim.SetInteger("MoveType", moveType);
        }
    }
    public override void OnAttack()
    {
        playerHealth.TakeDamage(zombieData.Damage);
        Debug.Log($"Player Take :{zombieData.Damage} damage");

        if (!playerHealth._isShieldActive)
        {
            GamePlayUI.Instance.ShowScratch();
        }
        else
        {
            GamePlayUI.Instance.ShieldEffectShow();
        }
    }
    public override void Attack()
    {
        base.Attack();
        int attackType = Random.Range(1, 3);
        anim.SetInteger("attackType", attackType);
    }
    public void SplatDamage()
    {
        var skill = Instantiate(splatSkill, firePos.position, Quaternion.identity);
        SkillSplat projectile = skill.GetComponent<SkillSplat>();

        projectile.SetTarget(playerTaget.position,zombieData.Damage);

    }
    public void SetBack()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position, 5f * 2 * Time.deltaTime);
    }
}
