﻿using System.Collections;
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
    [HideInInspector]
    public bool isInvokedInBossRoom = false;

    #region Player Variable
    protected Player player;
    #endregion

    #region State properties
    public bool IsPoisoned { get; set; }
    public bool isSlowed { get; set; }
    public bool isAlreadyElectrified;
    public bool isOnFire = false;
    
    public bool isBurned = false;
    #endregion

    #region Armes and special effect variable

    public static bool isPerturbateurIEM = false;
    public static bool isArretTemporel = false;
    
    #endregion


    #region  Animation 
    //Animator variable 
    public Animator animator;
    //Bool to Check If ready to start an another attack sequence
    protected bool attackAnimationPlaying = false;


    protected virtual void SetMouvementAnimationVariable()
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

        if (currentState == State.KnockedBack)
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

            animator.SetFloat("lastMoveX", targetSetter.target.position.x - gameObject.transform.position.x);
            animator.SetFloat("lastMoveY", targetSetter.target.position.y - gameObject.transform.position.y);
        }
    }

    //Methode permetant de lancer la séquence de d'attaque via l'animation
    protected virtual void PlayAttackAnim()
    {
        if (!attackAnimationPlaying && !isPerturbateurIEM)
        {
            attackAnimationPlaying = true;
            isAttacking = true;
            animator.SetTrigger("isAttacking");
        }
    }

    #endregion


    #region  PathFinding 

    [HideInInspector]
    public float nextWayPointDistance = 0.05f;
    [HideInInspector]
    public AIPath aIPath;
    [HideInInspector]
    public AIDestinationSetter targetSetter;
    [HideInInspector]
    public Transform target;

    protected void EnableEnemyMouvement()
    {
        aIPath.canMove = true;
        aIPath.canSearch = true;
    }

    protected void DisableEnemyMouvement()
    {
        aIPath.canMove = false;
    }
    #endregion


    #region Physics 
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    // Permet de fear
    protected virtual void Fear()
    {
        float moveSpeed = aIPath.maxSpeed * 100;
        if (direction == Vector3.zero) direction = (player.transform.position - gameObject.transform.position).normalized;

        rb.velocity = -direction * moveSpeed * Time.fixedDeltaTime;
    }

    // Coroutine qui knockBack l'ennemi
    public virtual IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        if (currentState == State.Charging) yield break;
        rb.AddForce(dir * knockBackForce);
        currentState = State.KnockedBack;
        yield return new WaitForSeconds(knockBackTime);
        currentState = State.Chasing;
        rb.velocity = Vector2.zero;
        aIPath.canMove = true;
        if (enemy.currentHealth <= 0) currentState = State.Death;
    }

    // Attack
    #endregion


    #region Health and Death
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
   

    // HealthBar
    public GameObject healthBarGFX;
    protected bool isHealthBarActive;
    [SerializeField]
    protected float timeBeforeDesactive;
    protected bool isDying = false;



    // Set health to maximum
    protected virtual void SetMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    // Prends les dégats
    public virtual void TakeDamage(float _damage)
    {
        TakeDamageSound();
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            isDying = true;
            StartCoroutine(DeathSate());

        }
    }

    // DeathCODE
    private IEnumerator DeathSate()
    {
        yield return new WaitForEndOfFrame();
        currentState = State.Death;
    }

    private void HasEnterDyingState()
    {
        DisableEnemyMouvement();
        rb.velocity = Vector2.zero;
        if (GetComponent<Collider2D>().enabled)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<Collider2D>())
                {
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("isDying");
            DieSound();
        }
    }

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
    // END DeathCode

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
    #endregion


    #region State && Transition
    // Distance ou l'ennemi repère le joueur
    protected float inSight = 10f;
    public bool isreadyToAttack = true;
    protected float attackDelay;
    protected bool isAttacking;
    protected float attackRange;
    protected bool isReadyToSwitchState;
    protected bool isSupposedToMoveAttacking = false;
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
    void SwitchBasicStates(State currentState)
    {
        switch(currentState)
        {
            default:
                rb.velocity = Vector2.zero;
            break;
            case State.Paralysed:
                // Animation
                DisableEnemyMouvement();
            break;

            case State.KnockedBack:
                DisableEnemyMouvement();
            break;

            case State.Freeze:
                // Animation
                DisableEnemyMouvement();
            break;

            case State.Feared:
                DisableEnemyMouvement();
            Fear();
            break;

            case State.Death:
                HasEnterDyingState();

            break;
            case State.Patrolling:
                if (aIPath.canMove)
            {
                DisableEnemyMouvement();
            }
            PlayerInSight();


            break;
        }
    }

    protected virtual void PlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            currentState = State.Chasing;
            EnableEnemyMouvement();
        }
    }

    protected void ShouldNotMoveDuringAttacking(bool isSupposedToMoveAttacking)
    {
        if (!isSupposedToMoveAttacking)
        {
            if (currentState == State.Chasing && !aIPath.canMove)
            {
                aIPath.canMove = true;
            }
            else if (currentState == State.Attacking && aIPath.canMove)
            {
                aIPath.canMove = false;
            }
        }

    }

    // Permet de savoir si il est en state Chase ou Attacking
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
        }
        else 
        {
                currentState = State.Chasing;
        }
    }


    #endregion

  

    #region Unity Mono

    protected virtual void Awake()
    {
        GetReference();
    }

    protected virtual void Start()
    {
        SetMaxHealth();
    }
    protected virtual void GetReference()
    {
        healthBarGFX.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        aIPath = GetComponent<AIPath>();
        currentState = State.Patrolling;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        
        targetSetter = GetComponent<AIDestinationSetter>();
        targetSetter.target = target;
        player = target.GetComponent<Player>();
        player = target.GetComponent<Player>();

        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }


    protected virtual void Update()
    {

        SwitchBasicStates(currentState);

        healthBar.SetHealth(currentHealth);
        DisplayBar();
        ShouldNotMoveDuringAttacking(isSupposedToMoveAttacking);

        SetMouvementAnimationVariable();
        GetLastDirection();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #endregion


    #region sound

    protected AudioManagerEffect audioManagerEffect;
    [SerializeField] protected string fireSound;
    [SerializeField] protected string takeDamageSound;
    [SerializeField] protected string dieSound;

    protected void FireSound()
    {
        if (audioManagerEffect != null)
            audioManagerEffect.Play(fireSound);
    }

    protected void TakeDamageSound()
    {
        if (audioManagerEffect != null)
            audioManagerEffect.Play(takeDamageSound);
    }

    protected void DieSound()
    {
        if (audioManagerEffect != null)
            audioManagerEffect.Play(dieSound);
    }


    #endregion 


}
