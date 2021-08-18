using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Wanderer.Utils;

public abstract class Characters : StateMachine
{
    [HideInInspector]
    public Animator animator;
    public bool IsPoisoned { get; set; }
    public bool IsBurned { get; set; }
    public bool IsSlowed { get; set; }
    public bool IsStuned { get; set; }

    public bool IsDying { get; set; }

    [HideInInspector]
    public Rigidbody2D rb;

    protected virtual void Awake()
    {
        GetReference();
        SetData();
        SetMaxHealth();
    }
    protected virtual void Start()
    {

    }

    #region Health System

    public int MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public virtual void TakeDamage(float damage, GameObject damageSource = null)
    {
        TakeDamageSound();
        TakeDammageParticleSystem();
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            IsDying = true;
            Die();
        }
        if (damageSource != null)
        {
            ApplyKnockBack(knockBackForceToApply, knockBackTime: 0.3f, damageSource);
        }
    }

    #region Particle System

    protected ParticleSystem hitParticleSystem;

    protected void TakeDammageParticleSystem()
    {
        if(hitParticleSystem != null)
        {
            // Do toggle
            // Utils.TogglePs(hitParticleSystem, true);
        }
    }

    #endregion

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
            if (!IsDying)
            {
                rb.AddForce(knockBackForce * dir * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                //DEATH CASE (Big knockback) j'ai pas trouv� mieux
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
    protected abstract void Die();
    #endregion


    #region Sounds
    protected AudioManagerEffect audioManagerEffect;
    [SerializeField] protected string takeDamageSound;
    [SerializeField] protected string dieSound;
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
