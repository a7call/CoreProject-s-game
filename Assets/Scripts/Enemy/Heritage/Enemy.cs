using System.Collections;
using UnityEngine;
using System;

public abstract class Enemy : Characters
{
    #region Room && dungeon related
    protected int difficulty;
    public int Difficulty
    {
        get
        {
            return difficulty;
        }
    }

    private Collider2D roomFloorCollider;
    public Collider2D RoomFloorCollider
    {
        get
        {
            return roomFloorCollider;
        }
        set
        {
            roomFloorCollider = value;
        }
    }
    #endregion

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
        SetState(new PatrollingState(this));
    }

    protected override void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        AIMouvement = GetComponent<AIMouvement>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Player = target.GetComponent<Player>();
        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }

    #region new States
    public bool CanFlee
    {
        get;
        set;
    } = false;
    public virtual void StartChasingState()
    {
        AIMouvement.ShouldMove = true;
    }
    public virtual void DoChasingState() { }
    public virtual void DoAttackingState() { }
    public virtual void DoPatrollingState() { }
    public virtual IEnumerator EndFleeingState() { yield return null; }

    // Distance ou l'ennemi repère le joueur
    protected float inSight = 10f;
    protected bool isAttacking;
    protected float attackRange;
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
        // Nothing to do for now.
        StateR.UpdateState();
    }

    private void FixedUpdate()
    {
        SetMouvementAnimation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #endregion

    #region  Animation 

    Vector2 lastPos = new Vector2();
    protected virtual void SetMouvementAnimation()
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

        // Direction to look to
        animator.SetFloat(EnemyConst.DIRECTION_X_CONST, AIMouvement.target.position.x - gameObject.transform.position.x);
        animator.SetFloat(EnemyConst.DIRECTION_Y_CONST, AIMouvement.target.position.y - gameObject.transform.position.y);
    }

   
    //Bool to Check If ready to start an another attack sequence
    protected bool attackAnimationPlaying = false;

    //Methode permetant de lancer la séquence de d'attaque via l'animation
    protected virtual void PlayAttackAnim()
    {
        if (!attackAnimationPlaying && !isAttacking)
        {
            attackAnimationPlaying = true;
            isAttacking = true;
            animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, true);
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
    #endregion


    #region Health and Death

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
        EnemyDeath();
        StopAllCoroutines();
        SetState(new DeathState(this));  
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
