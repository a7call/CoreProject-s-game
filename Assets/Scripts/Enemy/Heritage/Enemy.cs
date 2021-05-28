using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
/// <summary>
/// Classe mère des ennemis 
/// Elle contient une enum permettant d'indiquer le State de l'ennemi
/// Elle contient des fonctions permettant de gerer le pathfinding (UpdatePath() + MovetoPath() + OnPathComplete(Path p)). Pour avoir des détails se référer à Lopez
/// Une fonction de patrouille
/// Une fonction permettant de suivre le joueur si il est en range d'aggro
/// Une fonction permmettant de savoir si le joueur est en range d'aggro
/// Une fonction permettant d'initialiser le premier point de patrouille
/// Les fonctions nécessaires à la gestion de la vie de l'ennemi (se référer à Lopez )
/// </summary>
public abstract class Enemy : Characters
{
    protected int difficulty;
    public int Difficulty
    {
        get
        {
            return difficulty;
        }
    }

    #region Player Variable
    protected Player player;
    #endregion

    #region Unity Mono

    public event Action<GameObject> onEnemyDeath;

    protected override void Awake()
    {
        base.Awake();
        AddAnimationEvent("Death", "DestroyEnemy");    
    }

    protected override void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        aIMouvement = GetComponent<AIMouvement>();
        currentState = State.Patrolling;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<Player>();
        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }
    
  

    protected virtual void Update()
    {
        SwitchBasicStates(currentState);
        ShouldNotMoveDuringAttacking(isSupposedToMoveAttacking);

        // A MODIFIER SI ON TROUVE MIEUX
        if (IsStuned)
        {
            currentState = State.Stunned;
            return;
        }
    }
   
    private void FixedUpdate()
    {
        GetLastDirection();
        SetMouvementAnimationVariable();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (onEnemyDeath != null)
            onEnemyDeath(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #endregion

    #region  Animation 
    //Bool to Check If ready to start an another attack sequence
    protected bool attackAnimationPlaying = false;

    Vector2 lastPos = new Vector2();
    protected virtual void SetMouvementAnimationVariable()
    {

        if (AIMouvement.ShouldMove)
        {
            Vector2 trackVelocity = (rb.position - lastPos) * 50;
            lastPos = rb.position;
            animator.SetFloat("Speed", trackVelocity.sqrMagnitude);
        }
        else
        {
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
        animator.SetFloat("lastMoveX", AIMouvement.target.position.x - gameObject.transform.position.x);
        animator.SetFloat("lastMoveY", AIMouvement.target.position.y - gameObject.transform.position.y);
    }

    //Methode permetant de lancer la séquence de d'attaque via l'animation
    protected virtual void PlayAttackAnim()
    {
        if (!attackAnimationPlaying )
        {
            attackAnimationPlaying = true;
            isAttacking = true;
            animator.SetTrigger("isAttacking");
        }
    }
    #endregion


    #region  PathFinding
    protected AIMouvement aIMouvement;
    public AIMouvement AIMouvement
    {
        get
        {
            return aIMouvement;
        }
    }

    protected Transform target;
    public Transform Target
    {
        get
        {
            return target;
        }
    }
 
    #endregion


    #region Physics 
    // Coroutine qui knockBack l'ennemi
    public virtual IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime, Enemy enemy)
    {
        //CHANGER POUR IMPLUSE (PLUS ADAPTE)
        rb.AddForce(dir * knockBackForce, ForceMode2D.Force);
        State previousState = currentState;
        currentState = State.KnockedBack;
        yield return new WaitForSeconds(knockBackTime);
        if (isDying)
        {
            currentState = State.Death;
            yield break;
        }
        else
        {
            currentState = previousState;
        }
        // C'est DU SPARADRA
        if (this != null)
        {
            rb.velocity = Vector2.zero;
            AIMouvement.ShouldMove = true;
        }
        
    }

    // Attack
    #endregion


    #region Health and Death
    protected bool isDying = false;

    // Prends les dégats
    public override void TakeDamage(float damage)
    {
        foreach (var ability in PassiveObjectManager.currentAbilities)
        {
            ability.ApplyEffect(this);
        }
        base.TakeDamage(damage);

    }

    protected override void Die()
    {
        StartCoroutine(DeathSate());
        isDying = true;
    }
    private IEnumerator DeathSate()
    {
        yield return new WaitForEndOfFrame();
        
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
        currentState = State.Death;
    }
    protected virtual void DestroyEnemy()
    {
        if (isDying)
        {
            isDying = false;
            Destroy(gameObject);
        }
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
        Stunned,
        KnockedBack,
        Freeze,
        Feared,
        Charging,
    }
    void SwitchBasicStates(State currentState)
    {
        switch (currentState)
        {
            default:
                break;
            case State.Stunned:
                // Animation
                if (IsStuned)
                {
                    aIMouvement.ShouldMove = false;
                }
                else
                {
                    isInRange();
                }      
                break;

            case State.KnockedBack:
                aIMouvement.ShouldMove = false;
                break;

            case State.Freeze:
                // Animation
                aIMouvement.ShouldMove = false;
                break;

            case State.Death:
                aIMouvement.ShouldMove = false;
                //NOTHING ELSE TO DO
                break;

            case State.Patrolling:
                if (AIMouvement.ShouldMove)
                {
                    aIMouvement.ShouldMove = false;
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
            AIMouvement.ShouldMove = true;
        }
    }

    protected void ShouldNotMoveDuringAttacking(bool isSupposedToMoveAttacking)
    {
        if (!isSupposedToMoveAttacking)
        {
            if (currentState == State.Chasing )
            {
               AIMouvement.ShouldMove = true;
               
            }
            else if (currentState == State.Attacking )
            {
               AIMouvement.ShouldMove = false;
                
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) <= attackRange / 2)
            {
                AIMouvement.ShouldMove = false;
            }
            else if (Vector3.Distance(transform.position, target.position) >= attackRange)
            {
                AIMouvement.ShouldMove = true;
            }
        }

    }

    // Permet de savoir si il est en state Chase ou Attacking
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            aIMouvement.DirectionToTarget = Vector2.zero;
        }
        else 
        {
            currentState = State.Chasing;
        }
    }
    #endregion


    #region sound

    [SerializeField] protected string fireSound;

    protected void FireSound()
    {
        if (audioManagerEffect != null)
            audioManagerEffect.Play(fireSound);
    }
    #endregion
}
