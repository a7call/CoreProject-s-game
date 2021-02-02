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

    public bool isOnFire = false;

    public bool isreadyToAttack = true;
   
    public Animator animator;

    // Permet de vérifier si le monstre est dans la BossRoom
    [HideInInspector]
    public bool isInvokedInBossRoom = false;

    // pour l'épée electrique
    [HideInInspector]
    public bool isAlreadyElectrified;

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
        Charging,
    }

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

    // Permet de récupérer toutes les références utiles
    protected virtual void GetReference()
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
            default:
                rb.velocity = Vector2.zero;
                break;
            case State.Paralysed:
                // Animation
                aIPath.canMove = false;
                break;

            case State.KnockedBack:
                aIPath.canMove = false;
                break;

            case State.Freeze:
                // Animation
                aIPath.canMove = false;
                break;

            case State.Feared:
                aIPath.canMove = false;
                Fear();
                break;

            case State.Death:
                EnemyDie();
                break;
        }

        healthBar.SetHealth(currentHealth);
        DisplayBar();

        SetAnimationVariable();
        GetLastDirection();
    }

    // Permet d'envoyer les variables gérant l'animator
    protected virtual void SetAnimationVariable()
    {
        if (aIPath.canMove)
        {
            animator.SetFloat("HorizontalSpeed", aIPath.velocity.x);
            animator.SetFloat("VerticalSpeed", aIPath.velocity.y);
            float EnemySpeed = aIPath.velocity.sqrMagnitude;
            animator.SetFloat("Speed", EnemySpeed);
        }
        else
        {
            animator.SetFloat("HorizontalSpeed", 0);
            animator.SetFloat("VerticalSpeed", 0);
            float EnemySpeed = 0;
            animator.SetFloat("Speed", EnemySpeed);
        }

        //mettre d'autres conditions 
        if(currentState == State.Attacking )
        {
           // animator.SetBool("IsAttacking", true);
        }
        else
        {
           // animator.SetBool("IsAttacking", false);
        }
        
        if(currentState == State.KnockedBack)
        {
            //animator.SetBool("isTakingDamage", true);
        }
        else
        {
            //animator.SetBool("isTakingDamage", false);
        }
    }

    // Permet de récuperer la dernière direction
    protected virtual void GetLastDirection()
    {
        if (aIPath.desiredVelocity.x > 0.1 || aIPath.desiredVelocity.x < 0.1 || aIPath.desiredVelocity.y < 0.1 || aIPath.desiredVelocity.y > 0.1)
        {
            animator.SetFloat("LastMoveX", targetSetter.target.position.x - gameObject.transform.position.x);
            animator.SetFloat("LastMoveY", targetSetter.target.position.y - gameObject.transform.position.y);
        }
    }

    // Mouvement
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public bool isSlowed = false;
    [HideInInspector]
    public bool isBurned = false;
    // Distance ou l'ennemi repère le joueur
    protected float inSight;
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
    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    // Permet de fear
    protected virtual void Fear()
    {
        if(direction == Vector3.zero) direction = (playerMouvement.transform.position - gameObject.transform.position).normalized;
        rb.velocity = -direction * moveSpeed * Time.fixedDeltaTime;
    }

    // Attack
    protected float attackRange;
    protected bool isReadyToSwitchState;
    protected float timeToSwitch;
    protected bool isInTransition;

    // Couroutine qui laisse du temps entre les changements d'état
    protected IEnumerator transiChasing()
    {
        isInTransition = true;
        yield return new WaitForSeconds(timeToSwitch);
        aIPath.canMove = true;
        isReadyToSwitchState = true;
        isInTransition = false;
    }

    // Permet de savoir si il est en state Chase ou Attacking
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

    // Health

    // Vie actuelle
    [HideInInspector]
    public float currentHealth;
    // Vie initiale
    [HideInInspector]
    public int maxHealth;
    // Material d'indication pour un ennemi touché
    protected Material whiteMat;
    protected Material defaultMat;
    [SerializeField]
    protected HealthBar healthBar;
    // Sprite rendered de l'ennemi
    [SerializeField] protected SpriteRenderer spriteRenderer;

    // HealthBar
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

    protected bool isDying = false;


    // Mort de l'ennemi
    protected virtual void EnemyDie()
    {
        if (isDying)
        {
            // Animator
            animator.SetBool("isDying", true);
            isDying = false;
            SpawnRewards();
            nanoRobot();
            Destroy(gameObject);    
        }
    }

    // Prends les dégats
    public virtual void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth <= 0)
        {
            isDying = true;
            StartCoroutine(DeathSate());
            
        }
    }

    // Passe en state de Death
    private IEnumerator DeathSate()
    {
       yield return new WaitForEndOfFrame();
       currentState = State.Death;
    }

    protected void nanoRobot()
    {
        if (PlayerProjectiles.isNanoRobotModule)
        {
            NanoRobotModule nanoRobotModule = FindObjectOfType<NanoRobotModule>();
            nanoRobotModule.NanoRobotExplosion(gameObject.transform);
        }
    }
	
    protected void SpawnRewards()
    {
        if (!isInvokedInBossRoom)
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
    }

    // Couroutine white flash on hit
    protected virtual IEnumerator WhiteFlash()
    {
        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMat;
    }
   
    // Affiche la barre de vie
    protected void DisplayBar()
    {
        if (currentHealth < maxHealth && !isHealthBarActive)
        { 
            healthBarGFX.SetActive(true);
            isHealthBarActive = true;
        }
    }

    // Coroutine qui désactive la barre de vie
    protected IEnumerator WaitToDesactive()
    {
        yield return new WaitForSeconds(timeBeforeDesactive);
        healthBarGFX.SetActive(false);
        isHealthBarActive = false;
    }

    // Coroutine qui knockBack l'ennemi
    public IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        rb.AddForce(dir * knockBackForce);
        currentState = State.KnockedBack;
        yield return new WaitForSeconds(knockBackTime);
        if (enemy == null) yield break;  
        currentState = State.Chasing;
        if (enemy == null) yield break;
        rb.velocity = Vector2.zero;
        aIPath.canMove = true;
        if (enemy.currentHealth <= 0) currentState = State.Death;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }


}
