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

    [HideInInspector]
    public static bool isPerturbateurIEM = false;

    [HideInInspector]
    public static bool isArretTemporel = false;

    public State currentState;
    public enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        ShootingLaser,
        Paralysed,
        KnockedBack,
        Freeze,
        Feared
        
    }
    // pour l'épée electrique
    [HideInInspector]
    public bool isAlreadyElectrified;


    // PathFinding variable
    [HideInInspector]
    public float nextWayPointDistance = 0.05f;
    Path path;
    int currentWayPoint;
    bool reachedEndOfPath;
    Seeker seeker;
    Vector2 force;
    // end of Pathfinding variable


   

    protected virtual void Awake()
    {
        GetReference();
        StartPathing();
    }

    void GetReference()
    {
        healthBarGFX.SetActive(false);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMouvement = FindObjectOfType<PlayerMouvement>();

    }

    protected virtual void Update()
    {
       
        switch (currentState)
        {
            case State.Paralysed:
                // animation
                rb.velocity = Vector2.zero;
                break;

            case State.KnockedBack:

                break;

            case State.Freeze:
                //animation
                rb.velocity = Vector2.zero;
                break;

            case State.Feared:
                //animation
                Fear();
                break;

            
        }
        healthBar.SetHealth(currentHealth);
        DisplayBar();
       
    }

    protected virtual void FixedUpdate()
    {

        switch (currentState)
        {
            case State.Chasing:
               // MoveToPath();
                break;
        }
    }





    ///  Pathing code 
    void StartPathing()
    {
       // InvokeRepeating("UpdatePath", 0f, 0.1f);
    }
   protected virtual void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, targetPoint.position, OnPathComplete);
            
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;

        }
    }

    protected virtual void MoveToPath()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        force = dir * moveSpeed * Time.deltaTime;
        print(force.sqrMagnitude);
        
        rb.AddForce(force, ForceMode2D.Force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    /// End of Pathing code

   




    //Mouvement
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public bool isSlowed = false;
    // Destination pour patrouille
    [HideInInspector]
    public Transform targetPoint;
    // Distance ou l'ennemi repère le joueur
    protected float inSight;
    // Player
    //[HideInInspector]
    public Transform target;
    [HideInInspector]
    public Rigidbody2D rb;

    // Actualise le State en Chasing si le joueur est repéré
    protected virtual void PlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            currentState = State.Chasing;
            targetPoint = target;
        }
    }


    // Set le premier point de patrouille
    protected virtual void SetFirstPatrolPoint()
    {
        targetPoint = transform;
    }

   
    // Fear l'ennemi dans la direction opposée au joueur

    /// <summary>
    /// NE DEVRAIT PAS ETRE LA
    /// </summary>
    public Vector3 direction = Vector3.zero;
    private int fearDistance = 5;
    protected virtual void Fear()
    {   if(direction == Vector3.zero) direction = (playerMouvement.transform.position - gameObject.transform.position).normalized;
        rb.velocity = -direction * moveSpeed * Time.fixedDeltaTime;
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
            SpawnRewards();
        }
    }


    void SpawnRewards()
    {
        RewardSpawner rewardSpawner = FindObjectOfType<RewardSpawner>();
        rewardSpawner.RandomCoinReward(this.gameObject);
        rewardSpawner.SpawnKeyReward(this.gameObject);
        rewardSpawner.SpawnHeartReward(this.gameObject);
        rewardSpawner.SpawnAmoReward(this.gameObject);
        rewardSpawner.SpawnCoffreArmeReward(this.gameObject);
        rewardSpawner.SpawnCoffreModuleReward(this.gameObject);
        rewardSpawner.SpawnArmorReward(this.gameObject);
        Destroy(gameObject);
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

    //Attack

    protected float attackRange;

    protected bool isReadyToSwitchState;
    protected float timeToSwitch;
    protected bool isInTransition;
    protected IEnumerator transiChasing()
    {
        isInTransition = true;
        yield return new WaitForSeconds(timeToSwitch);
        isReadyToSwitchState = true;
        isInTransition = false;
    }

    
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isReadyToSwitchState = false;
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

    protected virtual void Aggro()
    {

    }





    public IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        rb.isKinematic = false;
        rb.AddForce(dir * knockBackForce);
        currentState = State.KnockedBack;
        yield return new WaitForSeconds(knockBackTime);
        if (enemy == null) yield break;  
        currentState = State.Attacking;
        if (enemy == null) yield break;
        rb.isKinematic = true;
        

    }




}
