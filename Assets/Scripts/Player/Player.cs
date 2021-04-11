using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wanderer.CharacterStats;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
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


    [Header("Player")]
    public CharacterStats mHealth;
    public CharacterStats moveSpeed;
    #endregion

    #region Enum
    public EtatJoueur currentEtat;
    public enum EtatJoueur
    {
        normal,
        fear,
        shopping,
        Grapping,
        Dashing,
        AFK,
    }
    #endregion

    protected virtual void Awake()
    {
        SetData();
        GetReference();
        SetMaxHealth();
        SetMaxEnergy();
        SetMaxEnergyBarUI();

    }

    #region Datas & reference
    protected void SetData()
    {
        maxHealth = playerData.maxHealth;
        maxStacks = playerData.maxStacks;
        mooveSpeed = playerData.mooveSpeed;
        dashForce = playerData.dashForce;
        stacksReloadTime = playerData.stacksReloadTime;
        DashTime = playerData.DashTime;


        emptyHearth = playerData.emptyHearth;
        halfHearth = playerData.halfHearth;
        fullHearth = playerData.fullHearth;
        imageArmor = playerData.imageArmor;
        halfArmor = playerData.halfArmor;
        fullArmor = playerData.fullArmor;
    }
    private void GetReference()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        HealthContent = canvas.transform.Find("HealthContent");
        image1 = HealthContent.Find("ImageFirstHP").GetComponent<Image>();
        image2 = HealthContent.Find("ImageSecondHP").GetComponent<Image>();
        image3 = HealthContent.Find("ImageThirdHP").GetComponent<Image>();
        Transform ArmorContent = canvas.transform.Find("ArmorContent");
        imageArmor = ArmorContent.Find("ImageArmor").GetComponent<Image>();
        activeObjectManager = GetComponentInChildren<ActiveObjectManager>();
        weaponManager = GetComponentInChildren<WeaponsManagerSelected>();
        inventory = GetComponentInChildren<Inventory>();
    }
    #endregion

    protected void Update()
    {
        Animation();
        AjustHhealth();
        UpdateUILife();

        StartCoroutine(EnergyReloading());
        if (currentStack > maxStacks)
        {
            energyBar.SetEnergy(maxStacks);
            currentStack = maxStacks;
            energyIsReloading = false;
            StopAllCoroutines();
        }

        if (isSpeedShoesModule && !SpeedAlreadyUp)
        {
            SpeedAlreadyUp = true;
            mooveSpeed *= SpeedMultiplier;
        }
        switch (currentEtat)
        {
            default:
                break;

            case EtatJoueur.normal:
                CheckInputs();
                //GetInputAxis();
                ClampMouvement(mouvement);

                break;

            case EtatJoueur.fear:
                break;

            case EtatJoueur.shopping:
                //Definir tout ce qu'on veut faire dedans
                break;

            case EtatJoueur.AFK:
                canDash = false;
                break;

            case EtatJoueur.Grapping:
                break;
        }

    }

    #region MOUVEMENT
    [HideInInspector] public Rigidbody2D rb;
     public Animator animator;
    public float mooveSpeed;
    private void FixedUpdate()
    {
        switch (currentEtat)
        {
            default:

                break;

            case EtatJoueur.normal:
                MovePlayer(mouvementVector * mooveSpeed * Time.fixedDeltaTime);
                break;

            case EtatJoueur.fear:
                break;

            case EtatJoueur.shopping:
                rb.velocity = Vector2.zero;
                break;

            case EtatJoueur.Grapping:
                break;

            case EtatJoueur.AFK:
                isShooting = true;
                rb.velocity = Vector2.zero;
                break;

            case EtatJoueur.Dashing:
                PiercedPocketActivation();
                break;
        }

    }

    #region Mouvement physics
    public Vector3 velocity = Vector3.zero;
    public float StartSmoothTime;
    public float StopSmoothTime;
    private Vector2 mouvementVector;
    private Vector2 mouvement;
    void MovePlayer(Vector2 _mouvement)
    {
        Vector3 targetVelocity = _mouvement;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, StartSmoothTime);
    }


    // Same Speed when Input (x,y)
    void ClampMouvement(Vector2 _mouvement)
    {

        mouvementVector = Vector2.ClampMagnitude(_mouvement, 1);
    }
    #endregion

    
    #region Modules
    public static bool isModuleInertie;
    protected bool SpeedAlreadyUp = false;
    public static bool isSpeedShoesModule;
    public static float SpeedMultiplier;
    public static bool isPiercedPocketModule;
    [HideInInspector]
    public static bool isArretTemporelActive = false;
    #endregion


    #region Animation
    protected Vector3 screenMousePos;
    protected Vector3 screenPlayerPos;
    Vector3 horizon = new Vector3(1, 0, 0);
    void Animation()
    {


        float playerSpeed = mouvement.sqrMagnitude;
        animator.SetFloat("Speed", playerSpeed);

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque 
        Vector3 dir = new Vector3((screenMousePos - screenPlayerPos).x, (screenMousePos - screenPlayerPos).y);

        float angle = Quaternion.FromToRotation(Vector3.left, horizon - dir).eulerAngles.z;


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
    #endregion


    #region Mouvement Inputs
    // Check If Player released Inputs
    void CheckInputs()
    {
        if (mouvement == Vector2.zero)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref velocity, StopSmoothTime);
        }
    }

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
        StartCoroutine(Dash());

        //PlayerControl variable;
        //variable.Player.Reload.actionMap.AddBinding("<Keyboard/a");
    }
    #endregion

    #endregion

    #region ENERGY

    #region Energy Management
    [HideInInspector]
    public int maxStacks;
    public bool energyIsReloading = false;
    public int currentStack;
    private EnergyBar energyBar;
    public float stacksReloadTime;

    public void SpendEnergy(int stackSpend)
    {
        currentStack -= stackSpend;
        energyBar.SetEnergy(currentStack);
    }
    void SetMaxEnergyBarUI()
    {
        energyBar = FindObjectOfType<EnergyBar>();
        energyBar.SetMaxEnergy(maxStacks);
    }
    void SetMaxEnergy()
    {
        currentStack = maxStacks;
    }

    public IEnumerator EnergyReloading()
    {
        if (!energyIsReloading && currentStack != maxStacks)
        {
            energyIsReloading = true;
            yield return new WaitForSeconds(stacksReloadTime);
            currentStack++;
            energyBar.SetEnergy(currentStack);
            energyIsReloading = false;
        }

    }
    #endregion

    #region Dash

    public float dashForce;
    protected float DashTime;
    public bool canDash = true;
    public float timeBetweenDashes = 0.5f;
    private IEnumerator Dash()
    {
        if (canDash && currentStack > 0)
        {
            Vector2 dir = new Vector2(mouvement.x, mouvement.y);
            if (dir != Vector2.zero)
            {
                currentEtat = EtatJoueur.Dashing;
                SpendEnergy(1);
                canDash = false;
                rb.AddForce(dir * dashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                LayerMask projectilLayerMask = 3;
                Physics2D.IgnoreLayerCollision(gameObject.layer, projectilLayerMask);
                yield return new WaitForSeconds(DashTime);
                Physics2D.IgnoreLayerCollision(gameObject.layer, projectilLayerMask, false);
                currentEtat = EtatJoueur.normal;
                yield return new WaitForSeconds(timeBetweenDashes);
                canDash = true;
            }

        }
    }


    void PiercedPocketActivation()
    {
        PiercedPocketModule pockets = FindObjectOfType<PiercedPocketModule>();
        if (pockets)
        {
            if (pockets.bombsReady) StartCoroutine(pockets.SpawnBombs());
        }
    }
    #endregion
    #endregion

    #region HEALTH
    #region Module
    public static bool isLastChanceModule = false;
    [HideInInspector] public static bool isPowerUp = false;
    private bool isPowerUpAlreadyUse = false;
    public static bool IsDontFuckWithMe = false;

    private void AjustHhealth()
    {
        if (isPowerUp && !isPowerUpAlreadyUse)
        {
            maxHealth = 8;
            currentHealth += 2;
            isPowerUpAlreadyUse = true;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        // RETIRER LE HEALTH IF LORSQUE L'ON AURA FAIT LA MORT DU JOUEUR
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
    #endregion


    #region Damage to player
    public SpriteRenderer graphics;
    protected bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if (currentArmor > 0)
            {
                currentArmor -= damage;
                StartCoroutine(InvincibilityDelay());
                StartCoroutine(InvincibilityFlash());
            }
            else
            {
                currentHealth -= damage;
                StartCoroutine(InvincibilityDelay());
                StartCoroutine(InvincibilityFlash());
            }
        }
        if (IsDontFuckWithMe)
        {
            DontFuckWithMeModule.DestroyAllEnemyInRange();
        }

        if (currentHealth <= 0 && !isLastChanceModule)
        {
            //A mettre ici par la suite;
            //image1.sprite = emptyHearth;
            //Debug.Log("Mort");
        }

        else if (currentHealth <= 0 && isLastChanceModule)
        {
            currentHealth += 2;
            isLastChanceModule = false;
        }
    }
    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
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


    #region Health and armor
    public int maxHealth;
    public int currentHealth;
    public int currentArmor;
    private int maxArmor = 2;
    public void AddLifePlayer(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void AddArmorPlayer(int _armor)
    {
        currentArmor += _armor;
        if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }
    }
   
    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }
    #endregion


    #region UI
    protected Image image1, image2, image3, image4, imageArmor;
    protected Sprite emptyHearth, halfHearth, fullHearth, halfArmor, fullArmor;
    protected Transform HealthContent;
    protected GameObject canvas;
    private void UpdateUILife()
    {
        if (currentArmor == maxArmor)
        {
            imageArmor.enabled = true;
            imageArmor.sprite = fullArmor;
        }
        else if (currentArmor == maxArmor - 1)
        {
            imageArmor.enabled = true;
            imageArmor.sprite = halfArmor;
        }
        else
        {
            imageArmor.enabled = false;
        }


        if (isPowerUp == false)
        {
            if (currentHealth == maxHealth)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
            }
            else if (currentHealth == maxHealth - 1)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = halfHearth;
            }
            else if (currentHealth == maxHealth - 2)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 3)
            {
                image1.sprite = fullHearth;
                image2.sprite = halfHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 4)
            {
                image1.sprite = fullHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 5)
            {
                image1.sprite = halfHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 6)
            {
                image1.sprite = emptyHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
            }
        }
        else
        {

            HealthContent.Find("ImagePowerUpHP").GetComponent<Image>().enabled = true;
            image4 = HealthContent.Find("ImagePowerUpHP").GetComponent<Image>();

            if (currentHealth == maxHealth)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = fullHearth;
            }
            else if (currentHealth == maxHealth - 1)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = halfHearth;
            }
            else if (currentHealth == maxHealth - 2)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = fullHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 3)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = halfHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 4)
            {
                image1.sprite = fullHearth;
                image2.sprite = fullHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 5)
            {
                image1.sprite = fullHearth;
                image2.sprite = halfHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 6)
            {
                image1.sprite = fullHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 7)
            {
                image1.sprite = halfHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
            else if (currentHealth == maxHealth - 8)
            {
                image1.sprite = emptyHearth;
                image2.sprite = emptyHearth;
                image3.sprite = emptyHearth;
                image4.sprite = emptyHearth;
            }
        }
    }
    #endregion
    #endregion

    #region Inputs attatck, coffre et interaction 

    #region Inputs

    WeaponsManagerSelected weaponManager;
    ActiveObjectManager activeObjectManager;
    Inventory inventory;
    protected bool isShooting = false;

    public PlayerInput playerInput;
    public void OnShoot()
    {
        if (!isShooting)
        {
            isShooting = true;

            if (weaponManager.isPlayingDistance)
            {

                //weaponManager.GetComponentInChildren<DistanceWeapon>().toShoot();
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
                //weaponManager.GetComponentInChildren<DistanceWeapon>().toShoot();
                weaponManager.GetComponentInChildren<IShootableWeapon>().OkToShoot = false;


            }
            isShooting = false;

        }



    }

    public void OnReload()
    {
        if (weaponManager.isPlayingDistance)
        {
            weaponManager.GetComponentInChildren<DistanceWeapon>().toReload();
        }
    }


    public void OnUseObject()
    {
        if (activeObjectManager.GetComponentInChildren<ActiveObjects>())
        {
            activeObjectManager.GetComponentInChildren<ActiveObjects>().ToUseModule();
        }
    }

    public void OnBlackHole()
    {
        inventory.SpawnBlackHole();
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
        ShopManager shopManager = FindObjectOfType<ShopManager>();

        if (OpenShop)
        {
            OpenShop = false;
            shopManager.OpenShop = false;
        }
        else
        {
            OpenShop = true;
            shopManager.OpenShop = true;
        }
    }

    public void OnPause()
    {
        PauseMenu.pause = true;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    public void OnSwitchToCac()
    {
        weaponManager.SwitchDistanceToCac();
    }

    public void OnSwitchToDistance()
    {
        weaponManager.SwitchCacToDistance();
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
    #endregion





}
