using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour, ICharacter
{
    public Animator animator;
    public bool IsPoisoned { get; set; }
    public bool IsBurned { get; set; }
    public bool IsSlowed { get; set; }
    public bool IsStuned { get; set; }

    public bool IsDying { get; set; }


    #region Health System
    protected int maxHealth;
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    protected float currentHealth;
    public int CurrentHealth { get => (int)currentHealth; set => currentHealth = value; }
    public virtual void TakeDamage(float damage)
    {
        TakeDamageSound();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            IsDying = true;
            Die();
        }
    }

    protected virtual void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

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
