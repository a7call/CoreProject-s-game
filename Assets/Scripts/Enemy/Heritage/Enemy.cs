using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
/// <summary>
/// Classe mère des ennemis 
/// Elle contient une enum permettant d'indiquer le State de l'ennemi
/// Elle contient des fonctions permettant de gerer le pathfinding (UpdatePath() + MovetoPath() + OnPathComplete(Path p)). Pour avoir des détails se référer à Lopez
/// Une fonction de patrouille
/// Une fonction permettant de suivre le joueur si il est en range d'aggro
/// Une fonction permmettant de savoir si le joueur est en range d'aggro
/// Une fonction permettant d'initialiser le premier point de patrouille
/// Les fonctions nécessaires à la gestion de la vie de l'ennemi (se référer à Lopez ou au tuto FR)
/// </summary>
public class Enemy : MonoBehaviour
{
    protected PlayerHealth playerHealth;
    protected PlayerMouvement playerMouvement;
    public static bool isPerturbateurIEM = false;
    public static bool isArretTemporel = false;
    public bool isreadyToAttack = true;

    public State currentState;
    public enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Death,
        ShootingLaser,
        Paralysed,
        KnockedBack,
        Freeze,
        Feared,
        Charging
    }

    // pour l'épée electrique
    [HideInInspector]
    public bool isAlreadyElectrified;


    // PathFinding variable
    [HideInInspector]
    public float nextWayPointDistance = 0.05f;
    [HideInInspector]
    public AIPath aIPath;
    [HideInInspector]
    public AIDestinationSetter targetSetter;
    // end of Pathfinding variable


   

    protected virtual void Awake()
    {
        GetReference();
    }

    void GetReference()
    {
        healthBarGFX.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();

    }

    protected virtual void Update()
    {
       
        switch (currentState)
        {
            case State.Paralysed:
                aIPath.canMove = false;
                break;

            case State.KnockedBack:
                aIPath.canMove = false;
                break;

            case State.Freeze:
                //animation
                aIPath.canMove = false;
                break;

            case State.Feared:
                aIPath.canMove = false;
                Fear();
                break;

            case State.Death:
                EnemyDie();
                currentState = State.Death;
                break;
        }
        healthBar.SetHealth(currentHealth);
        DisplayBar();
       
    }

  

    //Mouvement
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public bool isSlowed = false;
    // Distance ou l'ennemi repère le joueur
    protected float inSight;
    // Player
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Rigidbody2D rb;

    // Actualise le State en Chasing si le joueur est repéré
    protected virtual void PlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            currentState = State.Chasing;
        }
    }

    /// <summary>
    /// NE DEVRAIT PAS ETRE LA
    /// </summary>
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    protected virtual void Fear()

    {
        if(direction == Vector3.zero) direction = (playerMouvement.transform.position - gameObject.transform.position).normalized;
        rb.velocity = -direction * moveSpeed * Time.fixedDeltaTime;
    }

    //Attack

    protected float attackRange;
    protected bool isReadyToSwitchState;
    protected float timeToSwitch;
    protected bool isInTransition;
    protected IEnumerator transiChasing()
    {
        isInTransition = true;
        yield return new WaitForSeconds(timeToSwitch);
        aIPath.canMove = true;
        isReadyToSwitchState = true;
        isInTransition = false;
    }


    protected virtual void isInRange()
    {
        if (gameObject == null) return;
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isReadyToSwitchState = false;
            aIPath.canMove = false;
        }
        else
        {
            if (currentState == State.Attacking) StartCoroutine(transiChasing());
            if (isReadyToSwitchState)
            {
                currentState = State.Chasing;
            }

        }
    }
    // Face le player quand il le suit
    void FacePlayer()
    {
        //to do
    }


    //Health

    // Vie actuelle
    [HideInInspector]
    public float currentHealth;
    // Vie initial
    [HideInInspector]
    public int maxHealth;
    // Material d'indication pour un ennemi touché
    protected Material whiteMat;
    protected Material defaultMat;
    [SerializeField]
    protected HealthBar healthBar;
    // sprite rendered de l'ennemi
    [SerializeField] protected SpriteRenderer spriteRenderer;

    //healthBar
    public GameObject healthBarGFX;
    protected bool isHealthBarActive;
    [SerializeField]
    protected float timeBeforeDesactive;

    // Set health to maximum
    protected virtual void SetMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private bool isDying = false;
    protected virtual void EnemyDie()
    {
        if (isDying)
        {
            isDying = false;
            SpawnRewards();
            nanoRobot();
            Destroy(gameObject);    
        }
    }

    // A MODIFIER AVEC LES ARMES A PARTICULE
    [HideInInspector]
    public bool isTakingDot;
    protected float DotTime = 3f;
    public IEnumerator DotOnEnemy()
    {
        isTakingDot = true;
        yield return new WaitForSeconds(DotTime);
        isTakingDot = false;
    }
    //


    // prends les dammages
    public virtual void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth < 1)
        {
            isDying = true;
            currentState = State.Death;
        }
    }

    protected void nanoRobot()
    {
        if (PlayerProjectiles.isNanoRobotModule)
        {
            NanoRobotModule nanoRobotModule = FindObjectOfType<NanoRobotModule>();
            //nanoRobotModule.NanoRobotExplosion(gameObject.transform);
        }
    }
	
    protected void SpawnRewards()
    {
        RewardSpawner rewardSpawner = FindObjectOfType<RewardSpawner>();
        rewardSpawner.RandomCoinReward(this.gameObject);
        rewardSpawner.SpawnKeyReward(this.gameObject);
        rewardSpawner.SpawnHeartReward(this.gameObject);
        rewardSpawner.SpawnAmoReward(this.gameObject);
        rewardSpawner.SpawnCoffreArmeReward(this.gameObject);
        rewardSpawner.SpawnCoffreModuleReward(this.gameObject);
        rewardSpawner.SpawnArmorReward(this.gameObject);
    }

    // Couroutine white flash on hit
    protected virtual IEnumerator WhiteFlash()
    {

        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMat;

    }
    // Distance d'où l'ennemi peu lancer une attaque
   

    protected  void DisplayBar()
    {
        if(currentHealth >= maxHealth && isHealthBarActive)
        {
            StartCoroutine(WaitToDesactive());
        }
        else if (currentHealth < maxHealth && !isHealthBarActive)
        { 
            healthBarGFX.SetActive(true);
            isHealthBarActive = true;
        }
    }

    protected IEnumerator WaitToDesactive()
    {
        yield return new WaitForSeconds(timeBeforeDesactive);
        healthBarGFX.SetActive(false);
        isHealthBarActive = false;
    }


    public IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        aIPath.canMove = false;
        rb.AddForce(dir * knockBackForce);
        currentState = State.KnockedBack;
        yield return new WaitForSeconds(knockBackTime);
        if (enemy == null) yield break;  
        currentState = State.Chasing;
        if (enemy == null) yield break;
        rb.velocity = Vector2.zero;
        aIPath.canMove = true;


    }




}
