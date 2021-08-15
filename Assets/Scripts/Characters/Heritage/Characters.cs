using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

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
    public virtual void TakeDamage(float damage)
    {
        TakeDamageSound();
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            IsDying = true;
            Die();
        }
    }

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
