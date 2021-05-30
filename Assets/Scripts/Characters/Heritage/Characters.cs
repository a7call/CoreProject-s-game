using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
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

    private int maxHealth;
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public float currentHealth;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
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
    //Set datas of scriptableObject
    protected abstract void SetData();

    protected abstract void GetReference();
    protected abstract void Die();
    #endregion

    #region Animation
    protected void AddAnimationEvent(string name, string functionName, float time = 0)
    {
        AnimationClip Clip = null;
        var animClip = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in animClip)
        {
            if (clip.name == name)
            {
                Clip = clip;
                break;
            }
        }

        var _aEvents = new AnimationEvent();
        _aEvents.functionName = functionName;

        if (time != 0)
            _aEvents.time = time;
        else
            _aEvents.time = Clip.length;

        Clip.AddEvent(_aEvents);
    }
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
