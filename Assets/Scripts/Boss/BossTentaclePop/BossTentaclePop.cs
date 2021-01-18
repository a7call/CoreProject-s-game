using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;


/// <summary>
/// Calculer le délai en commentaire
/// Et voir pourquoi le cycle ne se relance pas dans la phase 2
/// Puis on est bon
/// </summary>

public class BossTentaclePop : Enemy
{
    [Header("Global Parameters")]
    [SerializeField] private BossScriptableObject BossData;
    
    private float restTime;
    
    private GameObject projectile; // Mettre le projectile classique
    private GameObject eggProjectile; // Projectile Egg qui invoque un parasite rampant
    private GameObject eggRunner; // Ennemi qui run dans une direction aléatoire et explose
    private GameObject eggPop; // C'est un nid qui va invoquer des monstres en continue jusqu'à ce qu'il soit détruit
    
    private Player player;

    public BossState currentBossState;

    public enum BossState
    {
        LoadingPhase,
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
        player.currentEtat = Player.EtatJoueur.shopping; // Etat AFK du joueur, il ne peut rien faire
        currentState = State.Chasing;
        currentBossState = BossState.LoadingPhase;
        aIPath.canMove = false;
        SetTimers();
        SetMaxHealth();

    }

    // Deux states uniquement, chasing + attacking
    protected override void Update()
    {
        switch (currentBossState)
        {
            case BossState.LoadingPhase:
                if (!isLoading)
                {
                    StartCoroutine(Loading());
                }
                break;

            case BossState.Phase1:
                ActualState();

                    if (isReadyToCycle && !priorityFirstAbility &&!prioritySecondAbility)
                    {
                        StartCoroutine(Cycle());
                    }
                    else if (isReadyToFirstAbility)
                    {
                        StartCoroutine(CanFirstAbility());
                    }
                    else if (isReadyToSecondAbility)
                    {
                        StartCoroutine(CanSecondAbility());
                    }
                break;

            case BossState.Phase2:
                DeathState();

                if(isReadyToCycle && !priorityFirstAbility &&!isReadyToThirdAbility)
                {
                    StartCoroutine(Cycle());
                }
                else if (isReadyToThirdAbility)
                {
                    StartCoroutine(CanThirdAbility());
                }
                else if (isReadyToFirstAbility)
                {
                    StartCoroutine(CanFirstAbility());
                }
                break;
        }

        switch (currentState)
        {
            case State.Chasing:
                isInRange();
            break;

            case State.Attacking:
                isInRange();
                if(!priorityFirstAbility && !prioritySecondAbility &&!isReadyToThirdAbility)
                {
                    StartCoroutine(CanShoot());
                }
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
        player = FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();
    }
    void SetTimers()
    {
        reloadDelayFirstAbility = firstActionTime + secondActionTime + thirdActionTime + maxSecondAbilityCount*0.25f ;
        firstAbilityTimer = firstActionTime + secondActionTime + thirdActionTime + starterTimer;

        reloadDelayThirdAbility = firstActionTime + secondActionTime + thirdActionTime + maxFirstAbilityCount*0.25f; //+ encore des timers de la premiere ability

        StartCoroutine(StarterCycle());
        StartCoroutine(StarterFirstAbility());
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
        eggProjectile = BossData.eggProjectile;
        eggRunner = BossData.eggRunner;
        eggPop = BossData.eggPop;
    }

    private void ActualState()
    {
        if (currentHealth > maxHealth / 2)
        {
            currentBossState = BossState.Phase1;
        }
        else
        {
            StopAllCoroutines();
            priorityFirstAbility = false;
            prioritySecondAbility = false;
            //priorityThirdAbility = true;
            isReadyToThirdAbility = true;
            isReadyToCycle = true;
            reloadDelayFirstAbility = firstActionTime + secondActionTime + thirdActionTime; // Calculer le nouveau reload
            currentBossState = BossState.Phase2;
        }
    }

    private void DeathState()
    {
        if (currentHealth < 1)
        {
            print("Die");
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

    private float starterTimer = 3f;
    private bool isLoading = false;
    private IEnumerator Loading()
    {
        isLoading = true;
        yield return new WaitForSeconds(starterTimer);
        player.currentEtat = Player.EtatJoueur.normal;
        currentBossState = BossState.Phase1;
    }
    private IEnumerator StarterCycle()
    {
        yield return new WaitForSeconds(starterTimer);
        aIPath.canMove = true;
        isReadyToCycle = true;
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
    private int countThirdAttack = 0;
    private int offset;
    private void Shoot()
    {
        if (inFirstAttack)
        {
            attackRange = BossData.attackRange; // Il faut qu'elle soit grande
            restTime = BossData.restTime;
            GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }

        if (inSecondAttack)
        {
            attackRange = 5f; // Il faut qu'elle soit petite
            restTime = 0.5f;
            nbProjectile = 5;
            angleTir = 40;
            offset = 20;

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
            attackRange = BossData.attackRange; // Il faut qu'elle soit grande
            restTime = 0.5f;
            nbProjectile = 20;
            angleTir = 360;
            countThirdAttack++;

            if (countThirdAttack % 2 == 0) 
            { 
                for (int i = 0; i < nbProjectile; i++)
                {
                    decalage = (angleTir / nbProjectile)*i;
                    GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.AngleAxis(decalage, Vector3.forward));
                    myProjectile.transform.parent = gameObject.transform;
                }
            }
            else
            {
                for (int i = 0; i < nbProjectile; i++)
                {
                    decalage = (angleTir / nbProjectile) * i;
                    offset = (angleTir/nbProjectile) / 2;
                    GameObject myProjectile = Instantiate(projectile, transform.position, Quaternion.AngleAxis(offset+decalage, Vector3.forward));
                    myProjectile.transform.parent = gameObject.transform;
                }
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

    private int firstAbilityCount = 0;
    private int maxFirstAbilityCount = 10;
    private bool isReadyToFirstAbility = false;
    private bool priorityFirstAbility = false;
    private float firstAbilityTimer ;
    private float reloadDelayFirstAbility;

    private IEnumerator StarterFirstAbility()
    {
        yield return new WaitForSeconds(firstAbilityTimer);
        isReadyToFirstAbility = true;
    }

    private void FirstAbility()
    {
        attackRange = BossData.attackRange;
        GameObject myProjectile = Instantiate(eggProjectile, transform.position, Quaternion.identity);
        myProjectile.transform.parent = gameObject.transform;
        firstAbilityCount++;
    }

    float timeBtwswitchAbility = 2f;
    private IEnumerator CanFirstAbility()
    {
        if (firstAbilityCount != maxFirstAbilityCount)
        {
            priorityFirstAbility = true;
            restTime = 0.25f;
            isReadyToFirstAbility = false;
            if (firstAbilityCount == 0) yield return new WaitForSeconds(timeBtwswitchAbility);
            FirstAbility();
            yield return new WaitForSeconds(restTime);
            isReadyToFirstAbility = true;
        }
        else if(firstAbilityCount == maxFirstAbilityCount) 
        {
            isReadyToFirstAbility = false;
            yield return new WaitForSeconds(timeBtwswitchAbility);
            prioritySecondAbility = true;
            isReadyToSecondAbility = true;
            priorityFirstAbility = false;
            yield return new WaitForSeconds(reloadDelayFirstAbility);
            isReadyToFirstAbility = true;
            firstAbilityCount = 0;
        }
    }

    private int secondAbilityCount = 0;
    private int maxSecondAbilityCount = 15;
    private bool isReadyToSecondAbility = false;
    private bool prioritySecondAbility = false;

    private void SecondAbility()
    {
        attackRange = BossData.attackRange;
        GameObject enemy = Instantiate(eggRunner, transform.position, Quaternion.identity);
        enemy.SetActive(true);
        secondAbilityCount++;
    }

    private IEnumerator CanSecondAbility()
    {
        if (secondAbilityCount != maxSecondAbilityCount)
        {
            restTime = 0.25f;
            isReadyToSecondAbility = false;
            //if (secondAbilityCount == 0) yield return new WaitForSeconds(timeBtwswitchAbility);
            SecondAbility();
            yield return new WaitForSeconds(restTime);
            isReadyToSecondAbility = true;
        }
        else
        {
            isReadyToSecondAbility = false;
            yield return new WaitForSeconds(timeBtwswitchAbility);
            prioritySecondAbility = false;
            secondAbilityCount = 0;
        }
    }

    private void ThirdAbility()
    {
        attackRange = BossData.attackRange;
        nbProjectile = 3;
        // Faire le if avec le modulo pour changer la position de starter des oeufs selon le nb de fois qu'il les a lancé
        for (int i = 0; i < nbProjectile; i++)
        {
            // Corriger la transform.position
            GameObject nest = Instantiate(eggPop, transform.position, Quaternion.AngleAxis(i*(360/nbProjectile),Vector3.forward));
            nest.SetActive(true);
            nest.transform.parent = gameObject.transform;
        }
    }

    private bool isReadyToThirdAbility = false;
    private bool priorityThirdAbility = false;
    private float reloadDelayThirdAbility;
    private IEnumerator CanThirdAbility()
    {
        isReadyToThirdAbility = false;
        yield return new WaitForSeconds(timeBtwswitchAbility);
        ThirdAbility();
        yield return new WaitForSeconds(timeBtwswitchAbility);
        isReadyToFirstAbility = true;
        yield return new WaitForSeconds(reloadDelayThirdAbility);
        isReadyToThirdAbility = true;
    }

}
