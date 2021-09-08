using System.Collections;
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
    #endregion

    #region Enum
    #endregion
    public GameObject RH;
    public GameObject LH;

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
        healthBar = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponentInChildren<PlayerHealthBar>();
    }

    #region Datas & reference
    protected override void SetData()
    {
        MaxHealth = playerData.maxHealth;
        MoveForce = playerData.moveForce;
        DashForce = playerData.dashForce;
        MaxAcceleration = playerData.maxAcceleration;
    }
    protected override void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        weaponManager = GetComponentInChildren<WeaponsManagerSelected>();
    }
    #endregion

    protected void Update()
    {
        Animation();
        AjustHhealth();
        healthBar.UpdateHealthUI(CurrentHealth, MaxHealth);
        ClampMouvement(mouvement);
        
    }

    #region MOUVEMENT
    private void FixedUpdate()
    {
        MovePlayer(mouvementVector);
    }
    #endregion





    #region Mouvement physics

    private Vector2 mouvementVector = Vector2.zero;
    private Vector2 mouvement = Vector2.zero;
    private float MoveForce = 0f;
    private float DashForce = 0f;
    private bool isDashing = false;
    private float MaxAcceleration = 0;

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
       mouvementVector = Vector2.ClampMagnitude(_mouvement, 1);
      
    }
    private IEnumerator DashCo()
    {
        if (mouvementVector == Vector2.zero)
            yield break;

        isDashing = true;
        rb.AddForce(mouvementVector * DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        StartCoroutine(GetComponentInChildren<DashEffects>().DashEffect(delayBetweenGhosts : 0.05f, mouvementVector));
        while (rb.velocity.magnitude >= MaxAcceleration + 5f)
        {
            yield return null;
        }
        isDashing = false;
    }

    #endregion

    #region Teleportation

    //private bool isTpReloaded = true;
    //private float dashRadius = 1.5f;
    //[SerializeField] private float timerTp = 3f;
    //private float alphaDelay = 0.1f;

    //[SerializeField] private Transform[] objectsModifiedByTp;

    //private IEnumerator ChangeAlpha()
    //{
    //    int currentWeapon = weaponManager.selectedDistanceWeapon;
    //    objectsModifiedByTp[objectsModifiedByTp.Length - 1] = weaponManager.transform.GetChild(currentWeapon);

    //    foreach (Transform obj in objectsModifiedByTp)
    //    {
    //        obj.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 0f);
    //    }

    //    isInvincible = true;

    //    for (int i = 1; i < 6; i++)
    //    {
    //        yield return new WaitForSeconds(alphaDelay);
    //        foreach (Transform obj in objectsModifiedByTp)
    //        {
    //            obj.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, (0.2f * i));
    //        }
    //    }

    //    isInvincible = false;
    //}

    //private IEnumerator ReloadingTp()
    //{
    //    isTpReloaded = false;
    //    yield return new WaitForSeconds(timerTp);
    //    isTpReloaded = true;
    //}

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
        float playerSpeed = mouvement.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);
        // position de la souris sur l'écran 
        var screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        var  screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
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
        mouvement.x = val.Get<float>();
    }

    public void OnVertical(InputValue val)
    {
        mouvement.y = val.Get<float>();
    }


    public void OnDash()
    {
        StartCoroutine(DashCo());
    }


    #endregion


    #region Damage to player
    private SpriteRenderer spriteRenderer;
    protected bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;
    public override void TakeDamage(float damage, GameObject damageSource = null)
    {
        if (!isInvincible)
        {
            base.TakeDamage(damage);
            StartCoroutine(InvincibilityDelay());
            StartCoroutine(InvincibilityFlash());
        }
    }
    public IEnumerator InvincibilityFlash()
    {
        //while (isInvincible)
        //{
        //    spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        //    yield return new WaitForSeconds(InvincibilityFlashDelay);
        //    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        //   
        //}
        yield return new WaitForSeconds(InvincibilityFlashDelay);
    }

    public IEnumerator InvincibilityDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibleDelay);
        isInvincible = false;
    }


    #endregion


    #region Health and armor
    private void AjustHhealth()
    {
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        // RETIRER LE HEALTH IF LORSQUE L'ON AURA FAIT LA MORT DU JOUEUR
        else if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
    }
    protected override void Die()
    {
        // TO IMPLEMENT
    }

    #endregion


    #region UI
    private PlayerHealthBar healthBar;
    #endregion


    #region Inputs

    WeaponsManagerSelected weaponManager;
    protected bool isShooting = false;

    public PlayerInput playerInput;
    public void OnShoot()
    {
        if (weaponManager == null)
            return;

        if (!isShooting)
        {
            isShooting = true;

            if (weaponManager.isPlayingDistance && weaponManager.distanceWeaponsList != null)
            {
                weaponManager.GetComponentInChildren<IShootableWeapon>().OkToShoot = true;

            }
            else if (weaponManager.isPlayingCac)
            {
                weaponManager.GetComponentInChildren<CacWeapons>().ToAttack();
            }
        }
        else
        {
            if (weaponManager.isPlayingDistance)
            {
                weaponManager.GetComponentInChildren<IShootableWeapon>().OkToShoot = false;
            }

            isShooting = false;
        }



    }

    public void OnReload()
    {
        if (weaponManager.isPlayingDistance)
        {
            weaponManager.GetComponentInChildren<ShootableWeapon>().toReload();
        }
    }

    public void OnSwitchAttackMode()
    {
        weaponManager.SwitchAttackMode();
    }

    public void OnOpenCoffre()
    {
        if (OpenCoffre)
        {
            OpenCoffre = false;
        }
        else
        {
            OpenCoffre = true;
        }
    }

    public void OnOpenShop()
    {
      
    }


    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    #endregion

    #region Coffre
    protected bool OpenCoffre = false;
    protected bool OpenShop = false;

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Coffre"))
        {
            if (OpenCoffre)
            {
                collision.GetComponent<Coffre>().OkToOpen = true;
            }
            else return;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Coffre"))
        {
            collision.GetComponent<Coffre>().OkToOpen = false;
        }
    }
    #endregion
}
