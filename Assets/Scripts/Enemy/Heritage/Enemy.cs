using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Wanderer.Utils;
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

    public Collider2D roomFloorCollider;
    public Collider2D RoomFloorCollider { get { return roomFloorCollider; }set { roomFloorCollider = value; } }

    public bool CanFlee
    {
        get;
        set;
    } = false;

    #region Player Variable
    public Player Player { get; private set; }
    #endregion

    #region Unity Mono

    public event Action<GameObject> onEnemyDeath;
    public void EnemyDeath()
    {
        if (onEnemyDeath != null)
        {
            onEnemyDeath(this.gameObject);
        }      
    }

    protected override void Awake()
    {
        base.Awake();
        Utils.AddAnimationEvent("Death", "DestroyEnemy", animator);
        SetState(new PatrollingState(this));
    }

    protected override void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        AIMouvement = GetComponent<AIMouvement>();
        currentState = State.Patrolling;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Player = target.GetComponent<Player>();
        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }

    #region new State 
    public virtual void StartChasingState()
    {
        AIMouvement.ShouldMove = true;
    }
    public virtual void DoChasingState() { }
    public virtual void DoAttackingState() { }
    public virtual void DoPatrollingState() { }
    public virtual IEnumerator EndFleeingState() { yield return null; }
    protected bool isOutOfAttackRange(float range)
    {
        if (!isAttacking && (Vector3.Distance(transform.position, target.position) > range))
        {
            SetState(new ChasingState(this));
            return true;
        }
        return false;
    }
    protected bool isInAttackRange(float range)
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            SetState(new AttackState(this));
            return true;
        }
        return false;
    }
    protected bool isInChasingRange(float range)
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            SetState(new ChasingState(this));
            return true;
        }

        return false;
    }
    #endregion


    protected virtual void Update()
    {
       // SwitchBasicStates(currentState);
       // ShouldNotMoveDuringAttacking(isSupposedToMoveAttacking);

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
        EnemyDeath();
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
            animator.SetFloat(EnemyConst.SPEED_CONST, trackVelocity.sqrMagnitude);
        }
        else
        {
            float EnemySpeed = 0;
            animator.SetFloat(EnemyConst.SPEED_CONST, EnemySpeed);
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
        animator.SetFloat(EnemyConst.DIRECTION_X_CONST, AIMouvement.target.position.x - gameObject.transform.position.x);
        animator.SetFloat(EnemyConst.DIRECTION_Y_CONST, AIMouvement.target.position.y - gameObject.transform.position.y);
    }

    //Methode permetant de lancer la séquence de d'attaque via l'animation
    protected virtual void PlayAttackAnim()
    {
        if (!attackAnimationPlaying && !isAttacking)
        {
            attackAnimationPlaying = true;
            isAttacking = true;
            animator.SetBool(EnemyConst.ATTACK_TRIGGER_CONST, true);
            // Attack Executed by animation event.
            
        }
    }
    #endregion


    #region  PathFinding

    public AIMouvement AIMouvement
    {
        get;
        protected set;
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
        yield return null;
        ////CHANGER POUR IMPLUSE (PLUS ADAPTE)
        //rb.AddForce(dir * knockBackForce, ForceMode2D.Force);
        //currentState = State.KnockedBack;
        //yield return new WaitForSeconds(knockBackTime);
        //if (isDying)
        //{
        //    currentState = State.Death;
        //    yield break;
        //}
        //else
        //{
        //    currentState = State.Chasing;
        //}
        //// C'est DU SPARADRA
        //if (this != null)
        //{
        //    rb.velocity = Vector2.zero;
        //    AIMouvement.ShouldMove = true;
        //}
        
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
                    AIMouvement.ShouldMove = false;
                }
                else
                {
                  
                }      
                break;

            case State.KnockedBack:
                AIMouvement.ShouldMove = false;
                break;

            case State.Freeze:
                // Animation
                AIMouvement.ShouldMove = false;
                break;

            case State.Death:
                AIMouvement.ShouldMove = false;
                //NOTHING ELSE TO DO
                break;

            case State.Patrolling:
                if (AIMouvement.ShouldMove)
                {
                    AIMouvement.ShouldMove = false;
                }
                break;
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
