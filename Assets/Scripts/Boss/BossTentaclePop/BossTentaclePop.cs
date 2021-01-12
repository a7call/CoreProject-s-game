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
        aIPath.canMove = false;
        SetMaxHealth();
        StartCoroutine(DelayToStart());
        //Shoot();
    }

    // Deux states uniquement, chasing + attacking
    protected override void Update()
    {

        switch (currentBossState)
        {
            case BossState.Phase1:
                ActualState();
                    if (isReadyToCycle)
                    {
                        StartCoroutine(Cycle());
                    }
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

        projectile = BossData.projectile;
    }

    private float starterTimer = 3f;
    private IEnumerator DelayToStart()
    {
        yield return new WaitForSeconds(starterTimer);
        aIPath.canMove = true;
        isReadyToCycle = true;
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

    private bool isReadyToCycle = false;
    private bool inFirstAttack = false;
    private bool inSecondAttack = false;
    private bool inThirdAttack = false;

    private float firstActionTime = 5f;
    private float secondActionTime = 3f;
    private float thirdActionTime = 3f;

    private IEnumerator Cycle()
    {
        isReadyToCycle = false;

        inFirstAttack = true;
        yield return new WaitForSeconds(firstActionTime);

        inFirstAttack = false;
        inSecondAttack = true;
        yield return new WaitForSeconds(secondActionTime);

        inSecondAttack = false;
        inThirdAttack = true;
        yield return new WaitForSeconds(thirdActionTime);

        inThirdAttack = false;

        isReadyToCycle = true;
    }

    private int nbProjectile;
    private int decalage;
    private int angleTir;
    private void Shoot()
    {
        if (inFirstAttack)
        {
            attackRange = BossData.attackRange;
            restTime = BossData.restTime;
            GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }

        if (inSecondAttack)
        {
            attackRange = 5f;
            restTime = 0.75f;
            nbProjectile = 5;
            angleTir = 40;
            int offset = 20;

            for (int i = 0; i < nbProjectile; i++)
            {
                decalage = (angleTir / (nbProjectile-1)) * i;
                float angleDecalage = decalage - offset;
                GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angleDecalage, Vector3.forward));
                myProjectile.transform.parent = gameObject.transform;
            }
        }

        if (inThirdAttack)
        {
            attackRange = 3f;
            restTime = 1f;
            nbProjectile = 20;
            angleTir = 360;

            for (int i = 0; i < nbProjectile; i++)
            {
                decalage = (angleTir / nbProjectile)*i;
                GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.AngleAxis(decalage, Vector3.forward));
                myProjectile.transform.parent = gameObject.transform;
            }
        }
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
