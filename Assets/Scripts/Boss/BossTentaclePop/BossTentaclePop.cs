using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class BossTentaclePop : Enemy
{
    [Header("Global Parameters")]
    [SerializeField] private BossScriptableObject BossData;
    private float restTime;
    private int nbTir;
    private GameObject projectile; // Mettre le projectile classique
    // Health paramaters
    // maxHealth, currentHealth, healthBar

    // General Variables
    // Animator, rb

    // Player reference
    // playerMouvement, playerHealth, target

    // Sprite Renderer
    // spriteRenderer

    // PathFinding Parameters

    // Projectile

    // Projectile parameters
    // Projectile de type Salive
    //[SerializeField] private GameObject saliveProjectile; // Ne pas faire comme ca, voir comment Lopez à fait
    // Projectile de type Pompe
    //[SerializeField] private GameObject[] listPumpProjectiles = new GameObject[3];
    // Projectile de type 360°
    // A configurer plutard. A priori, j'aimerai bien en tirer un nombre random (de 6 à 12 imaginons)


    //[Header("Pattern Parameters")]
    //[SerializeField] private bool isShooting;
    //[SerializeField] private bool isReadyToShoot;
    //[SerializeField] private float attackRange;

    //[Header("SecondPhase Parameters")]
    //[SerializeField] private float dps;

    public BossState currentBossState;

    public enum BossState
    {
        Phase1,
        Phase2,
    }

    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    private void Start()
    {
        currentState = State.Chasing;
        currentBossState = BossState.Phase1;
        SetMaxHealth();
        //Shoot();
    }

    // Deux states uniquement, chasing + attacking
    protected override void Update()
    {
        print(attackRange);
        if (!isInCycle)
        {
            StartCoroutine(Cycle());
        }

        switch (currentBossState)
        {
            case BossState.Phase1:
                ActualState();
                print("State1");
                break;
            case BossState.Phase2:
                ActualState();
                print("State2");
                break;
        }

        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                print("Chasing");
            break;

            case State.Attacking:
                isInRange();
                StartCoroutine(CanShoot());
                print("Attacking");
                //Shoot();
                break;
        }

        healthBar.SetHealth(currentHealth);
        SetAnimationVariable();
        GetLastDirection();
    }

    protected override void GetReference()
    {
        healthBarGFX.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }

    private void SetData()
    {
        maxHealth = BossData.maxHealth;
        whiteMat = BossData.whiteMat;
        defaultMat = BossData.defaultMat;

        attackRange = BossData.attackRange;
        restTime = BossData.restTime;
        timeToSwitch = BossData.timeToSwich;
        nbTir = BossData.nbTir;
        projectile = BossData.projectile;
    }

    private void ActualState()
    {
        if (currentHealth >= maxHealth / 2)
        {
            currentBossState = BossState.Phase1;
        }
        else
        {
            currentBossState = BossState.Phase2;
        }
    }

    protected override void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isShooting = true;
            isReadyToSwitchState = false;
            aIPath.canMove = false;
        }
        else
        {
            if (currentState == State.Attacking && !isInTransition && isreadyToAttack) StartCoroutine(transiChasing());
            if (isReadyToSwitchState)
            {
                currentState = State.Chasing;
                isShooting = false;
            }
        }
    }

    private bool isInCycle = false;
    private float firstActionTime = 5f;
    private float secondActionTime = 3f;
    private float thirdActionTime = 3f;

    private IEnumerator Cycle()
    {
        isInCycle = true;
        attackRange = BossData.attackRange;
        restTime = BossData.restTime;
        yield return new WaitForSeconds(firstActionTime);
        attackRange = 5f;
        restTime = 0.5f;
        yield return new WaitForSeconds(secondActionTime);
        attackRange = 3f;
        restTime = 0.5f;
        yield return new WaitForSeconds(thirdActionTime);
        isInCycle = false;
    }

    private void Shoot()
    {
        GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        myProjectile.transform.parent = gameObject.transform;
    }

    private bool isShooting = false;
    private IEnumerator CanShoot()
    {
        if (isShooting && isreadyToAttack)
        {
            isreadyToAttack = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isreadyToAttack = true;
        }
    }

}
