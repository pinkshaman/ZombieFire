using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Zombie : MonoBehaviour
{
    public string ZombieName;
    public BaseZombie zombieData;
    public ZombieProperties properties;
    public Animator anim;
    public AudioSource audioSource;
    public NavMeshAgent agent;
    public Transform playerTaget;
    public GameObject RageObj;
    public PlayerHealth playerHealth;
    public GameObject BoneRig;
    public bool isRage;
    public bool isGetHit;
    public bool isDead;
    public bool hasRisen = false;
    public bool isRising;
    public UnityEvent OnReachingRadius;
    public UnityEvent OnStartMoving;
    private bool _isMovingValue;

    public bool IsMoving
    {
        get => _isMovingValue;
        private set
        {
            if (_isMovingValue == value) return;
            _isMovingValue = value;
            OnIsMovingValueChange();
        }
    }

    private void OnIsMovingValueChange()
    {
        agent.isStopped = !_isMovingValue;
        anim.SetBool("isWalking", _isMovingValue);

        if (_isMovingValue)
        {
            OnStartMoving.Invoke();
        }
        else
        {
            OnReachingRadius.Invoke();
        }
    }

    public void Awake()
    {
        StartCoroutine(InitializeZombieData());
        playerTaget = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();

    }
    public IEnumerator InitializeZombieData()
    {
        while (ZombieManager.Instance == null)
        {
            yield return null;
        }

        zombieData = ZombieManager.Instance.GetZombieData(ZombieName);


        if (zombieData == null)
        {
            Debug.LogWarning($"{gameObject.name} chưa được gắn zombieData!");
        }

    }
    public void Start()
    {
        OnReachingRadius.AddListener(Attack);
        OnStartMoving.AddListener(StopAttack);
        agent.speed = zombieData.Speed;
        isRage = false;
        isGetHit = false;
        isDead = false;
        RageObj.SetActive(false);
        BoneRig.SetActive(true);
    }

    public virtual void CheckHeight()
    {
        if (hasRisen) return;
        hasRisen = true;
        Rising();
    }

    public virtual void CheckOffMesh()
    {
        if (agent.isOnOffMeshLink)
        {
            var EndPos = agent.currentOffMeshLinkData.endPos;
            var StarPos = agent.currentOffMeshLinkData.startPos;
            transform.position = Vector3.Lerp(StarPos, EndPos, 0.1f * Time.deltaTime);
            agent.Warp(EndPos);
            Rising();
        }
    }
    public void Rising()
    {
        agent.isStopped = true;
        isRising = true;
        anim.SetTrigger("Rise");
    }
    public void IsRising()
    {
        isRising = false;
    }
    public virtual void Attack()
    {
        anim.SetBool("isAttacking", true);
        agent.isStopped = true;
    }
    public virtual void StopAttack()
    {
        anim.SetBool("isAttacking", false);
        agent.isStopped = false;
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void OnAttack()
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
    public void OnGetHit()
    {
        AnmGetHit();
        if (isRage) return;
        isRage = true;
        PlaySound(properties.clipRage);
        RageObj.SetActive(true);
        agent.speed *= 1.5f;

    }
    public IEnumerator GetHit()
    {
        StopMove();
        anim.SetTrigger("GetHit");
        yield return new WaitForSeconds(1.5f);
        if (!isDead) Move();
        isGetHit = false;
    }
    public void AnmGetHit()
    {
        if (isGetHit) return;
        isGetHit = true;
        StartCoroutine(GetHit());
    }
    public void CheckDistance()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, playerTaget.position);
        IsMoving = distanceToPlayer > zombieData.RangedAtk;
    }

    public virtual void Die(bool isHeadShot)
    {
        if (isDead) return;
        isDead = true;
        StopMove();
        Debug.Log("Zombie Dead");
        PlaySound(properties.clipDie);
        RageObj.SetActive(false);
        BoneRig.SetActive(false);

        anim.SetBool("isDead", true);

        if (isHeadShot)
        {
            anim.SetTrigger("HeadShot");
        }
        else
        {
            int deathType = Random.Range(1, 3);
            anim.SetInteger("DeathType", deathType);
        }
        Destroy(gameObject, 2.0f);
    }

    public virtual void Move()
    {
        if (isDead || agent == null || !agent.isOnNavMesh) return;
        agent.isStopped = false;
        agent.SetDestination(playerTaget.position);
        if(isRage)
        {
            int moveType = Random.Range(4, 7);
            anim.SetInteger("MoveType", moveType);
        }
        else
        {
            int moveType = Random.Range(1, 3);
            anim.SetInteger("MoveType", moveType);
        }
    }
    public virtual void StopMove()
    {
        agent.ResetPath();
        agent.isStopped = true;
    }
    public void FacingPlayer()
    {
        Vector3 direction = (playerTaget.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public virtual void Update()
    {
        if (isDead) return;
        CheckDistance();
        CheckOffMesh();
        CheckHeight();

        if (IsMoving)
        {
            Move();
        }
        else
        {
            Attack();

        }

    }
}
