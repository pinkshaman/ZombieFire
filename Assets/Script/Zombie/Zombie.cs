using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
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
    public bool isRage;
    public bool isGetHit;
    public bool isDead;
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

    //public void Awake()
    //{
    //    zombieData = ZombieManager.Instance.GetZombieData(ZombieName);
    //    playerTaget = GameObject.FindGameObjectWithTag("Player").transform;
    //    agent = GetComponent<NavMeshAgent>();
    //    anim = GetComponent<Animator>();
    //    playerHealth = FindObjectOfType<PlayerHealth>();
    //}
    public void Start()
    {
        zombieData = ZombieManager.Instance.GetZombieData(ZombieName);
        playerTaget = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        OnReachingRadius.AddListener(Attack);
        OnStartMoving.AddListener(StopAttack);
        agent.speed = zombieData.Speed;
        isRage = false;
        RageObj.SetActive(false);
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
    public void OnAttack()
    {
        playerHealth.TakeDamage(zombieData.Damage);
        Debug.Log($"Player Take :{zombieData.Damage} damage");
        PlayerManager.Instance.playerUI.ShowScratch();
    }
    public void Rising()
    {
        anim.SetTrigger("Rise");
    }
    public void OnGetHit()
    {
        isGetHit = true;
        anim.SetTrigger("GetHit");
    }
    public void Rage()
    {
        if (!isGetHit) return;
        RageObj.SetActive(true);
        isRage = true;     
    }
    public void CheckDistance()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, playerTaget.position);
        IsMoving = distanceToPlayer > zombieData.RangedAtk;
    }
   
    public virtual void Die()
    {
        isDead = true;
        StopMove();
        Debug.Log("Zombie Dead");
        anim.SetBool("isDead", true);
        RageObj.SetActive(false);
        Destroy(gameObject, 3.0f);
    }
    public virtual void Move()
    {      
        agent.SetDestination(playerTaget.position);
    }
    public virtual void StopMove()
    {
        agent.isStopped = true;
        anim.SetBool("isWalking", false);
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
        if(isDead) return;
        CheckDistance();
        Rage();
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
