﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wanderer.CharacterStats;
using UnityEngine.InputSystem;
using Wanderer.Utils;

public class Player : Characters
{
    public PlayerScriptableObjectScript playerData;

    #region Stats
    [Header("Projectile")]
    public CharacterStats damage;
    public CharacterStats projectileSpeed;
    public CharacterStats dispersion;
    public CharacterStats knockBackForce;
    public CharacterStats knockBackTime;
    public CharacterStats explosionDamage;
    public CharacterStats imolationDamage;
    public CharacterStats slow;

    [Header("Chance of drops")]
    public CharacterStats chanceToDropCoins;
    public CharacterStats chanceToDropObjects;
    public CharacterStats chanceToDropAmmos;
    public CharacterStats chanceToDropHeart;

    [Header("Weapon")]
    public CharacterStats reloadSpeed;
    public CharacterStats magSize;
    public CharacterStats cadence;
    public CharacterStats range;
    public CharacterStats attackSpeed;
    public CharacterStats ammoStock;

    [Header("CacWeapon")]
    public CharacterStats attackRadius;
    public CharacterStats attackRange;

    [Header("Player")]
    public CharacterStats mHealth;
    public CharacterStats moveForce;
    public CharacterStats dashForce;
    public CharacterStats DashStacks;
    #endregion

    public GameObject RH;
    public GameObject LH;

    public IShootableWeapon weapon;
    protected override void Awake()
    {
        base.Awake();
        if (RH != null && LH != null)
        {
            RH.SetActive(false);
            LH.SetActive(false);

        }
    }
    protected override void Start()
    {
        base.Start();
        PoolManager.GetInstance().CreatePool(executeEffect, 3);
        healthBar = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponentInChildren<PlayerHealthBar>();
    }

    #region Datas & reference
    protected override void SetData()
    {
        MaxHealth = playerData.maxHealth;
        MoveForce = playerData.moveForce;
        DashForce = playerData.dashForce;
        MaxDashNumber = playerData.maxDashNumber;
        DashReloadTime = playerData.dashReloadTime;
        MaxAcceleration = playerData.maxAcceleration;
    }
    protected override void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponManager = GetComponentInChildren<WeaponsManagerSelected>();
        ProjectileCollider = GetComponent<Collider2D>();
    }
    #endregion

    protected void Update()
    {
        Animation();
        healthBar.UpdateHealthUI(CurrentHealth, MaxHealth);
        ClampMouvement(mouvementVector);

        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();

    }
    private void FixedUpdate()
    {
        MovePlayer(mouvement);
    }

    #region Mouvement physics

    private Vector2 mouvement = Vector2.zero;
    private Vector2 mouvementVector = Vector2.zero;
    private float MoveForce { get; set; }

    private bool isDashing = false;
    private float MaxAcceleration { get; set; }


    void MovePlayer(Vector2 _mouvement)
    {
        if (rb.velocity.magnitude > MaxAcceleration && !isDashing)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxAcceleration);

        Vector2 MovementVector = _mouvement * MoveForce * Time.fixedDeltaTime;

        rb.AddForce(MovementVector, ForceMode2D.Impulse);
    }

    // Same Speed when Input (x,y)
    void ClampMouvement(Vector2 _mouvement)
    {
        mouvement = Vector2.ClampMagnitude(_mouvement, 1);
    }
    private float DashForce { get; set; }
    private int MaxDashNumber { get; set; }
    private int CurrentNumberOfDash { get; set; }
    private float DashReloadTime { get; set; }

    Coroutine DashReloadCo;

    Coroutine EnableColliderCo;

    private IEnumerator DashCo()
    {
        Vector2 dashDirection = mouvement.normalized;

        if (dashDirection == Vector2.zero || CurrentNumberOfDash >= MaxDashNumber)
            yield break;

        AudioManagerEffect.GetInstance().Play(AudioConst.PLAYER_DASH_EFFECT, this.gameObject, 0.2f);
        SelectDashReloadType();

        isDashing = true;
        CurrentNumberOfDash++;

        ProjectileCollider.enabled = false;

        rb.AddForce(dashDirection * DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        StartCoroutine(GetComponentInChildren<DashEffects>().DashEffect(delayBetweenGhosts: 0.05f, mouvement));

        while (rb.velocity.magnitude >= MaxAcceleration + 5f)
        {
            yield return null;
        }

        EnableCollider();

        isDashing = false;
    }


    private IEnumerator DashReload(bool isFirstDash)
    {

        if (isFirstDash)
            yield return new WaitForSeconds(DashReloadTime);
        else
            yield return new WaitForSeconds(DashReloadTime * 2);

        CurrentNumberOfDash = 0;

    }

    void EnableCollider()
    {
        if (EnableColliderCo != null)
        {
            StopCoroutine(EnableColliderCo);
        }

        EnableColliderCo = StartCoroutine(EnableProjectileColliderToleranceCo());
    }

    private IEnumerator EnableProjectileColliderToleranceCo()
    {
        yield return new WaitForSeconds(0.1f);
        ProjectileCollider.enabled = true;
        EnableColliderCo = null;
    }

    void SelectDashReloadType()
    {
        if (CurrentNumberOfDash == 0)
        {
            DashReloadCo = StartCoroutine(DashReload(isFirstDash: true));
        }
        else
        {
            StopCoroutine(DashReloadCo);
            DashReloadCo = StartCoroutine(DashReload(isFirstDash: false));
        }
    }


    #endregion

    #region Animation

    Vector3 playerMouseDir = Vector3.zero;

    //Animation event sounds
    void PlayFootStepFX()
    {
        AudioManagerEffect.GetInstance().Play("FootStep", this.gameObject);
    }

    //
    void Animation()
    {
        float playerSpeed = mouvementVector.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);
        // position de la souris sur l'écran 
        var screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        var screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque 
        playerMouseDir = new Vector3((screenMousePos - screenPlayerPos).x, (screenMousePos - screenPlayerPos).y);

        float angle = Quaternion.FromToRotation(Vector3.left, Vector3.right - playerMouseDir).eulerAngles.z;

        if (gameObject.name == "Player2")
        {
            if (weaponManager.isRH)
            {
                RH.SetActive(true);
                LH.SetActive(false);
                animator.SetBool("isRH", true);
            }
            else
            {
                RH.SetActive(false);
                LH.SetActive(true);
                animator.SetBool("isRH", false);
            }

            if (angle > 45 && angle <= 135)
            {
                animator.SetFloat("VerticalSpeed", 1);
                animator.SetFloat("HorizontalSpeed", 0);
            }
            else if (angle > 135 && angle <= 180)
            {
                animator.SetFloat("HorizontalSpeed", 1);
                animator.SetFloat("VerticalSpeed", 0);
            }
            else if (angle > 135 && angle <= 225)
            {
                animator.SetFloat("HorizontalSpeed", -1);
                animator.SetFloat("VerticalSpeed", 0);
            }

            else if (angle > 225 && angle <= 315)
            {
                animator.SetFloat("VerticalSpeed", -1);
                animator.SetFloat("HorizontalSpeed", 0);
            }
            else if (angle > 315 && angle <= 360)
            {
                animator.SetFloat("VerticalSpeed", 0);
                animator.SetFloat("HorizontalSpeed", -1);
            }
            else if (angle > 0 && angle <= 45)
            {
                animator.SetFloat("VerticalSpeed", 0);
                animator.SetFloat("HorizontalSpeed", 1);
            }
        }
        else
        {

            if (angle > 45 && angle <= 135)
            {
                animator.SetFloat("VerticalSpeed", 1);
                animator.SetFloat("HorizontalSpeed", 0);
            }
            else if (angle > 135 && angle <= 225)
            {
                animator.SetFloat("HorizontalSpeed", -1);
                animator.SetFloat("VerticalSpeed", 0);
            }

            else if (angle > 225 && angle <= 315)
            {
                animator.SetFloat("VerticalSpeed", -1);
                animator.SetFloat("HorizontalSpeed", 0);
            }
            else
            {
                animator.SetFloat("HorizontalSpeed", 1);
                animator.SetFloat("VerticalSpeed", 0);
            }
        }

    }
    #endregion

    #region Mouvement Inputs
    public void OnHorizontal(InputValue val)
    {
        mouvementVector.x = val.Get<float>();
    }

    public void OnVertical(InputValue val)
    {
        mouvementVector.y = val.Get<float>();
    }

    public void OnDash()
    {
        StartCoroutine(DashCo());
    }

    #endregion

    #region Execution
    public LayerMask enemyLayer;
    public GameObject executeEffect;
    public void OnExecute()
    {
        float maxExecutionDistance = 3f;

        var monsterToExecute = GetMonsterToExecute(ref maxExecutionDistance);
        
        if (monsterToExecute != null)
        {
            transform.position += Utils.GetRelativePositionOfAnObject(transform, monsterToExecute.transform, 0.5f, maxExecutionDistance);
            PoolManager.GetInstance().ReuseObject(executeEffect, monsterToExecute.transform.position, Quaternion.identity);
            monsterToExecute.TakeDamage(100, this.gameObject);
        }

    }

    private Enemy GetMonsterToExecute(ref float maxExecutionDistance)
    {
        List<Enemy> canBeExecuted = new List<Enemy>();
        var Monsters = Physics2D.CircleCastAll(transform.position, maxExecutionDistance, Vector2.zero, Mathf.Infinity, enemyLayer);

        foreach (var monster in Monsters)
        {
            Enemy monsterScript = monster.transform.GetComponent<Enemy>();

            if (monsterScript.IsExecutable && !monsterScript.IsDying)
                canBeExecuted.Add(monsterScript);
        }
       
        Enemy monsterToExecute = null;

        foreach (var monster in canBeExecuted)
        {
            var distancePlayerMonster = Vector3.Distance(transform.position, monster.transform.position);

            if (maxExecutionDistance > distancePlayerMonster)
            {
                monsterToExecute = monster;
                maxExecutionDistance = distancePlayerMonster;
            }
        }
        if (monsterToExecute != null)
            return monsterToExecute;
        else
            return null;
    }
    #endregion

    #region Damage to player
    protected bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;

    public override void TakeDamage(float damage, GameObject damageSource = null)
    {
        if (!isInvincible)
        {
            base.TakeDamage(damage);
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            CameraController.instance.StartShakeG(0.1f, 0.1f);
            StartCoroutine(PlayTakeDamageAnimation());
        }
        if(CurrentHealth <= 0 && !IsDying)
        {
            IsDying = true;
            Die();
        }
    }

    protected override IEnumerator PlayTakeDamageAnimation()
    {
        StartCoroutine(InvincibilityDelay());
        StartCoroutine(InvincibilityFlash());
        yield return null;
    }
    public IEnumerator InvincibilityFlash()
    {        
        while (isInvincible)
        {
            sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    public IEnumerator InvincibilityDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibleDelay);
        isInvincible = false;
    }

    #endregion

    #region UI
    private PlayerHealthBar healthBar;
    #endregion

    #region Inputs

    WeaponsManagerSelected weaponManager;
    protected bool isShooting = false;

    public PlayerInput playerInput;
    bool isholding = false;
    public void Shoot()
    {
        weapon.StartShootingProcess(shotValue: 0);
    }

    public void OnSpecialShoot()
    {
        weapon.StartShootingProcess(shotValue: 1);
    }

    public void OnReload()
    {
        weapon.toReload();
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    #endregion


    

    protected override void Die()
    {
        Debug.LogWarning("YOU ARE DEAD");
    }
}
