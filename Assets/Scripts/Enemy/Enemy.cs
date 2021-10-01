using System.Collections;
using UnityEngine;
using System;
using Wanderer.Utils;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(AudioSource))]
public abstract class Enemy : Characters, IMonster
{
    [Header("Materials")]
    [SerializeField]
    public Material executionMaterial;
    [SerializeField]
    protected Material hitMaterial;
    

    #region Room && dungeon related

    public int Difficulty { get; set; }
    public Collider2D RoomFloorCollider { get; set; }

    #endregion

    #region Player Variable
    public Player Player { get; private set; }
    #endregion

    #region Unity Mono


    public abstract IMonsterData GetMonsterData();

    public event Action<GameObject> onEnemyDeath;
    public void EnemyDeath(GameObject gameObject)
    {
        if (onEnemyDeath != null)
        {
            onEnemyDeath(this.gameObject);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetState(new SpawningState(this));
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(AllowFleeing());
    }
    private IEnumerator AllowFleeing()
    {
        yield return new WaitForSeconds(1f);
        CanFlee = true;
    }

    protected override void GetReference()
    {
        RigidBodySetUp();
        AIMouvement = GetComponent<AIMouvement>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Player = target.GetComponent<Player>();
        hitAnimator = Utils.FindGameObjectInChildWithTag(this.gameObject, "HitAnimations").GetComponent<Animator>();
    }

   
    private void RigidBodySetUp()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.drag = 100;
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
    public virtual IEnumerator StartFleeingState() { yield break; }

    // Distance ou l'ennemi repère le joueur
    protected float inSight = 10f;

 
    public bool isAttacking;
    [HideInInspector]
    public bool isReadyToAttack = true;

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

    public IEnumerator RestCo(Animator animator)
    {
        if (isAttacking)
        {
            isAttacking = false;
            animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, false);
            // delay before next Shoot
            yield return new WaitForSeconds(GetMonsterData().RestTime);
            isReadyToAttack = true;
            // gestion de l'animation d'attaque
            attackAnimationPlaying = false;
        }   
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
    public bool attackAnimationPlaying = false;

    //Methode permetant de lancer la séquence d'attaque via l'animation
    protected virtual void PlayAttackAnim(Animator animator)
    {
        if (!attackAnimationPlaying && !isAttacking)
        {
            attackAnimationPlaying = true;
            isAttacking = true;
            animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, true);
            // Attack Executed by animation event.
        }
    }

    [HideInInspector]
    public Animator hitAnimator { get; set; }

    private void PlayHitAnim()
    {
        //animator.SetTrigger(EnemyConst.HIT_TRIGGER_CONST);
        hitAnimator.SetTrigger(EnemyConst.HIT_TRIGGER_CONST);
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

    #region Health and Death

    bool isTakingDamage = false;
    // Prends les dégats
    public override void TakeDamage(float damage, GameObject damageSource = null)
    {
        PlayHitAnim();
        base.TakeDamage(damage, damageSource);
        if (CurrentHealth <= -10 && !IsDying)
        {  
            IsExecutable = false;
            IsDying = true;
            SetState(new DeathState(this));
        }
    }

    Material ExcutableMaterialInstance { get; set; }
    public IEnumerator SwitchToExecutionableState()
    {
       
        if (executionMaterial == null)
            yield break;

        ExcutableMaterialInstance = new Material(executionMaterial);
        sr.material = ExcutableMaterialInstance;
       
        var color1 = executionMaterial.GetColor("_OutlineColor");
        var color2 = executionMaterial.GetColor("_OutlineColor2");

        while (true)
        {
            sr.material.SetColor("_OutlineColor", color1);
            yield return new WaitForSeconds(0.2f);
            sr.material.SetColor("_OutlineColor", color2);
            yield return new WaitForSeconds(0.2f);
        }

    }

    protected override IEnumerator PlayTakeDamageAnimation()
    {
      
        if (isTakingDamage)
            yield break;

        Material currentMat = sr.material; ;

        isTakingDamage = true;
        sr.material = hitMaterial;
        yield return new WaitForSeconds(0.05f);

        if (IsExecutable)
            sr.material = ExcutableMaterialInstance;
        else if (IsDying)
            sr.material = BaseMaterial;
        else
            sr.material = currentMat;

        transform.localScale = new Vector3(0.9f, 1.1f, 1);
        yield return new WaitForSeconds(0.08f);
        transform.localScale = new Vector3(1.1f, 0.9f, 1);
        yield return new WaitForSeconds(0.04f);
        transform.localScale = new Vector3(1, 1, 1);
        isTakingDamage = false;

    }

    protected override void StartExecutableState()
    {        
        //CoroutineManager.GetInstance().StartCoroutine(KnockCo(knockBackForceToApply, -dir, knockBackTime: 0.3f));
        SetState(new ExecutableState(this));
    }

    protected override void Die()
    {
        return;
    }

    #endregion

}
