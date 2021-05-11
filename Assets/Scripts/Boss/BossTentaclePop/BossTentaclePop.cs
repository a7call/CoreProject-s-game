using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

/// <summary>
/// Le BossTentaclePop est basé sur deux states
/// Il utilise un pattern d'attaque classique ainsi que trois compétences différentes
/// Pour le Cycle, il tire des projectiles de PM, puis des projectiles de pompe, puis des projectiles à 360°
/// La première compétence est une succession de tirs de projectiles qui invoquent des parasites rampants si ils touchent le joueur
/// La deuxième compétence est une succession de mobs explosifs qui courent dans des directions randoms
/// La troisième compétence est l'invocation de nids qui vont faire pop différents ennemis classiques
/// </summary>

// Commentaires pour quand on reprendra le boss
// Rajouter la dispersion
// Voir pour les animations
// Voir pour l'intro et la mort du boss (dont ses rewards)
// Voir l'équilibrage du boss et des compétences
// Changer l'état Shopping du joueur par l'état AFK
// Voir les effets des armes spéciales sur le boss (freeze, dot, etc..) qui ne doivent pas marcher


public class BossTentaclePop : Enemy
{
    // Paramètres globaux du Boss
    [Header("Global Boss Parameters")]
    [SerializeField] private BossScriptableObject BossData;
    public BossState currentBossState; // Permet d'avoir plusieurs états pour le boss
    
    // Les différents objets utilisés par le Boss
    private GameObject projectile; // Mettre le projectile classique
    private GameObject eggProjectile; // Projectile Egg qui invoque un parasite rampant
    private GameObject eggRunner; // Ennemi qui run dans une direction aléatoire et explose
    private GameObject eggPop; // C'est un nid qui va invoquer des monstres en continue jusqu'à ce qu'il soit détruit
    [SerializeField] private GameObject randomSpawner; // Objet qui va gérer les endroits d'invocations des eggPop
    

    // Tous les timers des différentes coroutines
    [HideInInspector]
    [Header("Timers")]
    private float starterTimer = 3f; // Timer de lancement
    private float firstAbilityTimer ; // Timer de lancement de la première compétence
    private float timeBtwswitchAbility = 2f; // Temps qui permet de switch entre deux compétences
    private float dieTimer = 5f;

    // Timers liés au Cycle d'attaque
    private float firstActionTime = 5f; // Temps pendant lequel il effectue des tirs de FM
    private float secondActionTime = 3f; // Temps pendant lequel il effectue des tirs de pompe
    private float thirdActionTime = 3f; // Temps pendant lequel il effectue des tirs à 360°.


    // Tous les delay des différentes coroutines
    [HideInInspector]
    [Header("reloadDelay")]
    private float reloadDelayFirstAbility; // Delay de chargement de la première compétence
    private float reloadDelayThirdAbility; // Delay de chargement de la seconde compétence


    // Toutes les booléans
    [HideInInspector]
    [Header("Booleans")]
    private bool isLoading = false; // Boolean du chargement

    // Booleans du cycle
    private bool isReadyToCycle = false; // Boolean qui permet de savoir si le Cycle d'attaque peut etre lancé
    private bool inFirstAttack = false; // Boolean qui permet de gérer la première attaque du Cycle
    private bool inSecondAttack = false; // Boolean qui permet de gérer la seconde attaque du Cycle
    private bool inThirdAttack = false; // Boolean qui permet de gérer la troisième attaque du Cycle
    private bool isShooting = false; // Boolean qui permet de savoir si le Boss tire

    // Booleans des compétences
    private bool isCastingAbility = false; // Boolean qui permet de savoir si une compétence est en cours
    private bool isReadyToFirstAbility = false; // Boolean qui permet de savoir si la première compétence est prête à être lancée
    private bool isReadyToSecondAbility = false; // Boolean qui permet de savoir si la seconde compétence est prête à être lancée
    private bool isReadyToThirdAbility; // Boolean qui permet de savoir si la troisième compétence est prête à être lancée


    // Tous les compteurs
    [HideInInspector]
    [Header("Counter")]
    // Compteur des attaques du Cycle
    private int countThirdAttack = 0; // Compteur lié à le troisième attaque. Il permet de décaller un coup sur deux
    
    // Compteurs des compétences
    private int firstAbilityCount = 0; // Compteur de la première compétence
    private int maxFirstAbilityCount = 10; // Compteur max de la première compétence
    private int secondAbilityCount = 0; // Compteur de la seconde compétence
    private int maxSecondAbilityCount = 15; // Compteur max de la seconde compétence


    // Toutes les différents variables des projectilles
    [HideInInspector]
    [Header("Projectile Parameters")]
    private int nbProjectile; // Indique le nombre à tirer
    private float restTime; // Indique le délai entre deux projectiles
    private int decalage; // Angle de décallage entre les projectiles
    private int angleTir; // Angle total sur lequel on tir
    private int offset; // Permet d'aligner le projectile central sur le joueur



    public enum BossState
    {
        LoadingPhase,
        Phase1,
        Phase2,
        DeathPhase,
    }

    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    protected override void Start()
    {
        base.Start();
        gameObject.GetComponent<Enemy>().isInvokedInBossRoom = true;
        player.currentEtat = Player.EtatJoueur.shopping; // Etat AFK du joueur, il ne peut rien faire
        currentState = State.Chasing;
        currentBossState = BossState.LoadingPhase;
        aIPath.canMove = false;
        SetTimers();
        SetMaxHealth();
    }

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

                    if (isReadyToCycle && !isCastingAbility)
                    {
                        StartCoroutine(Cycle1());
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

                if (isReadyToCycle && !isCastingAbility)
                {
                    StartCoroutine(Cycle2());
                }
                else if (isReadyToThirdAbility)
                {
                    StartCoroutine("CanThirdAbility");
                }
                else if (isReadyToFirstAbility)
                {
                    StartCoroutine("CanFirstAbilityState2");
                }
                break;

            case BossState.DeathPhase:
                if (isDying) StartCoroutine(BossIsDead());
                break;
        }

        switch (currentState)
        {
            case State.Chasing:
                isInRange();
            break;

            case State.Attacking:
                isInRange();
                if(!isCastingAbility)
                {
                    StartCoroutine(CanShoot());
                }
                break;

            case State.Death:
                player.currentEtat = Player.EtatJoueur.shopping; // Etat AFK du joueur où il ne peut plus tirer non plus
                aIPath.canMove = false;
                break;
        }

       // healthBar.SetHealth(currentHealth);
        SetMouvementAnimationVariable();
        GetLastDirection();
    }

    // Permet d'avoir les références
    protected override void GetReference()
    {
       // healthBarGFX.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        player = FindObjectOfType<Player>();
        player = FindObjectOfType<Player>();
        player = FindObjectOfType<Player>();
    }
    
    // Permet de Set les Data
    protected override void SetData()
    {
       // maxHealth = BossData.maxHealth;

        attackRange = BossData.attackRange;
        restTime = BossData.restTime;
       // timeToSwitch = BossData.timeToSwich;

        projectile = BossData.projectile;
        eggProjectile = BossData.eggProjectile;
        eggRunner = BossData.eggRunner;
        eggPop = BossData.eggPop;
    }
    
    // Permet de déclarer les timers et de lancer les coroutines de Start
    private void SetTimers()
    {
        reloadDelayFirstAbility = firstActionTime + secondActionTime + thirdActionTime + maxSecondAbilityCount*0.25f ;
        firstAbilityTimer = firstActionTime + secondActionTime + thirdActionTime + starterTimer;

        reloadDelayThirdAbility = firstActionTime + secondActionTime + thirdActionTime + maxFirstAbilityCount * 0.25f + 2*timeBtwswitchAbility;

        StartCoroutine(StarterFirstAbility());
    }

    // Cette coroutine permet de lancer le combat
    private IEnumerator Loading()
    {
        isLoading = true;
        yield return new WaitForSeconds(starterTimer);
        aIPath.canMove = true;
        isReadyToCycle = true;
        player.currentEtat = Player.EtatJoueur.normal;
        currentBossState = BossState.Phase1;
    }
    
    // Permet de lancer la première compétence
    private IEnumerator StarterFirstAbility()
    {
        yield return new WaitForSeconds(firstAbilityTimer);
        isReadyToFirstAbility = true;
    }

    // Coroutine qui permet de faire une pause
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(timeBtwswitchAbility);
    }
  
    // Permet de savoir si le Boss est en State 1 ou 2
    private void ActualState()
    {
        if (CurrentHealth > MaxHealth / 2)
        {
            currentBossState = BossState.Phase1;
        }
        else
        {
            isCastingAbility = true;
            StartCoroutine(Pause());
            isReadyToThirdAbility = true;
            isReadyToCycle = false;
            firstAbilityCount = 0;
            countThirdAttack = 0;
            currentBossState = BossState.Phase2;
            isreadyToAttack = true;
        }
    }

    // Permet de savoir si le Boss est mort
    private void DeathState()
    {
        if (CurrentHealth <= 0)
        {
            StopCoroutine("CanThirdAbility");
            StopCoroutine("CanFirstAbilityState2");
            isCastingAbility = true;
            isReadyToFirstAbility = false;
            isReadyToSecondAbility = false;
            isReadyToThirdAbility = false;
            currentBossState = BossState.DeathPhase;
            currentState = State.Death;
            isDying = true;
        }
    }

    // Permet de gérer la mort du Boss
    private IEnumerator BossIsDead()
    {
        isDying = false;
        // Animation de mort
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Drop du boss
        yield return new WaitForSeconds(dieTimer);
        player.currentEtat = Player.EtatJoueur.normal;
        Destroy(gameObject);
    }

    // Permet de savoir si le Boss est en Chasing ou Attacking
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
         //   if (currentState == State.Attacking && !isInTransition && isreadyToAttack) StartCoroutine(transiChasing());
            if (isReadyToSwitchState)
            {
                currentState = State.Chasing;
                isShooting = false;
            }
        }
    }

    // Permet de gérer le Cycle1
    private IEnumerator Cycle1()
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

    // Permet de gérer le Cycle2 
    private IEnumerator Cycle2()
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

        yield return new WaitForSeconds(timeBtwswitchAbility);
        isReadyToCycle = true;
    }

    // Différents shoots des Cycles
    private void Shoot()
    {
        if(inFirstAttack)
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

    // Première compétence
    private void FirstAbility()
    {
        attackRange = BossData.attackRange;
        GameObject myProjectile = Instantiate(eggProjectile, transform.position, Quaternion.identity);
        myProjectile.transform.parent = gameObject.transform;
        firstAbilityCount++;
    }

    // Permet de gérer la première compétence dans le Cycle1
    private IEnumerator CanFirstAbility()
    {
        if (firstAbilityCount != maxFirstAbilityCount)
        {
            isCastingAbility = true;
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
            isReadyToSecondAbility = true;
            yield return new WaitForSeconds(reloadDelayFirstAbility);
            isReadyToFirstAbility = true;
            firstAbilityCount = 0;
        }
    }

    // Seconde compétence
    private void SecondAbility()
    {
        attackRange = BossData.attackRange;
        GameObject enemy = Instantiate(eggRunner, transform.position, Quaternion.identity);
        enemy.transform.parent = gameObject.transform;
        enemy.SetActive(true);
        secondAbilityCount++;
    }

    // Permet de gérer la seconde compétence
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
            isCastingAbility = false;
            secondAbilityCount = 0;
        }
    }

    // Troisième compétence
    private void ThirdAbility()
    {
        attackRange = BossData.attackRange;
        nbProjectile = 3;
        for (int i = 0; i < nbProjectile; i++)
        {
            GameObject nest = Instantiate(eggPop, randomSpawner.GetComponent<SpawnerObjects>().sp[i], Quaternion.identity);
            nest.transform.parent = gameObject.transform;
            nest.SetActive(true);
        }
    }

    // Permet de gérer la troisième compétence
    private IEnumerator CanThirdAbility()
    {
        isCastingAbility = true;
        aIPath.canMove = false;
        randomSpawner.SetActive(true);
        isReadyToThirdAbility = false;
        yield return new WaitForSeconds(timeBtwswitchAbility);
        ThirdAbility();
        aIPath.canMove = true;
        randomSpawner.SetActive(false);
        isReadyToFirstAbility = true;
        yield return new WaitForSeconds(reloadDelayThirdAbility);
        isReadyToThirdAbility = true;
    }

    // Permet de gérer la première compétence dans le Cycle2
    private IEnumerator CanFirstAbilityState2()
    {
        if (firstAbilityCount != maxFirstAbilityCount)
        {
            restTime = 0.25f;
            isReadyToFirstAbility = false;
            if (firstAbilityCount == 0) yield return new WaitForSeconds(timeBtwswitchAbility);
            FirstAbility();
            yield return new WaitForSeconds(restTime);
            isReadyToFirstAbility = true;
        }
        else if (firstAbilityCount == maxFirstAbilityCount)
        {
            isReadyToFirstAbility = false;
            yield return new WaitForSeconds(timeBtwswitchAbility);
            isCastingAbility = false;
            isReadyToCycle = true;
            firstAbilityCount = 0;
        }
    }

}
