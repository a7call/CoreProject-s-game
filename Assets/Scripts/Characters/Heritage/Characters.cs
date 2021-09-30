using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Wanderer.Utils;

[RequireComponent(typeof(AudioSource))]
public abstract class Characters : StateMachine
{
    public string Name;
    [HideInInspector]
    public Animator animator;
    public bool IsPoisoned { get; set; }
    public bool IsBurned { get; set; }
    public bool IsSlowed { get; set; }
    public bool IsStuned { get; set; }

    public bool IsDying { get; set; }
    public bool IsExecutable { get; set; }

    public Material BaseMaterial { get; private set; }
    
    [HideInInspector]
    public SpriteRenderer sr;
    protected AudioSource audioSource { get; private set; }

    [HideInInspector]
    public Rigidbody2D rb;
    protected Collider2D ProjectileCollider { get; set; } 

    protected virtual void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        BaseMaterial = sr.material;
        GetReference();
        SetData();
        SetMaxHealth();
    }
    protected virtual void Start()
    {
        this.gameObject.name = Name;
    }
    public void PlayDeathEffect()
    {
        AudioManagerEffect.GetInstance().Play(gameObject.name+ "Death", this.gameObject);
    }
    #region Health System

    public int MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public virtual void TakeDamage(float damage, GameObject damageSource = null)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0 && !IsExecutable)
        {
            IsExecutable = true;
            StartExecutableState();
        }
        if (damageSource != null)
        {
            ApplyKnockBack(knockBackForceToApply, knockBackTime: 0.3f, damageSource);
        }
        StartCoroutine(PlayTakeDamageAnimation());
    }
 

    #region Physics 
    bool isKnockedBack = false;
    public float knockBackForceToApply { get; protected set; }
    // Coroutine qui knockBack l'ennemi

    void ApplyKnockBack(float knockBackForce, float knockBackTime, GameObject damageSource)
    {
        var dir = (damageSource.transform.position - transform.position).normalized;
        CoroutineManager.GetInstance().StartCoroutine(KnockCo(knockBackForce, -dir, knockBackTime));
    }
    Vector3 test = Vector3.zero;
    public virtual IEnumerator KnockCo(float knockBackForce, Vector3 dir, float knockBackTime)
    {
        if (isKnockedBack)
            yield break;

        isKnockedBack = true;
        CoroutineManager.GetInstance().StartCoroutine(KnockBackDurationCo(knockBackTime));
        while (isKnockedBack)
        {
            if (rb == null)
                yield break;

            if (!IsDying)
            {
                rb.AddForce(knockBackForce * dir * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                //DEATH CASE (Big knockback) j'ai pas trouvé mieux
                float knockBackDyingMultiplier = 3;
                rb.AddForce(knockBackDyingMultiplier * knockBackForce * dir * Time.deltaTime, ForceMode2D.Impulse);
            }
            yield return null;
        }

    }
    IEnumerator KnockBackDurationCo(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        isKnockedBack = false;
    }
    #endregion

    protected virtual void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
    //Set datas of scriptableObject
    protected abstract void SetData();

    protected abstract void GetReference();
    protected abstract void StartExecutableState();
    protected abstract void Die();

    protected abstract IEnumerator PlayTakeDamageAnimation();
    #endregion

}
