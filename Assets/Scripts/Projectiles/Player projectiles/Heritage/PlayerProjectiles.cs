using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerProjectiles : MonoBehaviour
{

    protected GameObject playerGO;
    protected Player player;
    protected WeaponsManagerSelected weaponManager;
    protected Weapons weapon;
    protected Rigidbody2D rb;
    

    #region Stats
    protected float damage;
    protected float knockBackForce;
    protected float knockBackTime;
    protected float projectileSpeed;
    [HideInInspector]
    public float dispersion;
    protected LayerMask weaponLayer;
    #endregion

    


    protected virtual void Awake()
    {
        
        GetReferences();
        SetData();
        
    }
    protected virtual void Start()
    {
        Launch();

    }

    protected virtual void Update()
    {

        if (isInteligentAmmoModule)
        {
            getNewDir(this.gameObject);
        }


    }


    #region Datas && reférences

    protected void GetReferences()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
        weaponManager = playerGO.transform.GetComponentInChildren<WeaponsManagerSelected>();
        weapon = weaponManager.gameObject.transform.GetComponentInChildren<Weapons>();
        rb = GetComponent<Rigidbody2D>();
        audioManagerEffect = FindObjectOfType<AudioManagerEffect>();
    }

    void SetData()
    {
        damage = player.damage.Value;
        knockBackTime = player.knockBackTime.Value;
        projectileSpeed = player.projectileSpeed.Value;
        knockBackForce = player.knockBackForce.Value;
        weaponLayer = weapon.enemyLayer;
    }
    #endregion

   
    #region Move logic
    protected Vector3 directionTir;
    protected virtual void Launch()
    {

        directionTir = Quaternion.AngleAxis(dispersion, Vector3.forward) * transform.right;
        rb.velocity = directionTir * projectileSpeed;
        
    }
    #endregion


    #region Collision logic

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        ImpactSound();
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, directionTir, knockBackTime, enemy));
            //Modules
            ModuleProcs(enemy);
        }
         Destroy(gameObject);
    }
    #endregion

    #region Sound

    protected AudioManagerEffect audioManagerEffect;
    [SerializeField] protected string impactSound;

    protected void ImpactSound()
    {

    if (audioManagerEffect != null)
            audioManagerEffect.Play(impactSound);
    }
    #endregion


    #region Module
    public static bool isNuclearExplosionModule;
    public static bool isAtomiqueExplosionModule;
    public static float explosionDamageMultiplier;

    // ExplosionModule
    public static bool isExplosiveAmo = false;
    public static float explosiveRadius;
    public static LayerMask hitLayer;
    public static int explosionDamage;

    //ImolationModule
    public static bool isImolationModule = false;

    //CryoModule
    public static bool isCryoModule = false;

    //ParaModule
    public static bool isParaModule = false;

    //InteligentAmoModule
    public static bool isInteligentAmmoModule = false;
    //NanoRobotModule
    public static bool isNanoRobotModule = false;

    //RocketAmmoModule
    [HideInInspector]
    protected bool AmmoSpeedAlreadyUp = false;
    [HideInInspector]
    public static bool isRocketAmmoModule;
    [HideInInspector]
    public static float SpeedMultiplier;
    protected virtual void ModuleProcs(Enemy enemy)
    {


    }

    //IntelligentAmoModule
    private float angulSpeed = 200f;
    private GameObject lockEnemy;
    public void getNewDir(GameObject proj)
    {
        if (InteligentAmmoModule.LockEnemy(proj) != null) lockEnemy = InteligentAmmoModule.LockEnemy(proj);
        if (lockEnemy != null)
        {
            if (lockEnemy == null) return;
            Vector2 direction = (lockEnemy.transform.position - proj.transform.position);
            direction.Normalize();
            float rotationAmount = Vector3.Cross(direction, (transform.up * directionTir.y + transform.right * directionTir.x)).z;
            rb.angularVelocity = -rotationAmount * angulSpeed;
            rb.velocity = (transform.up * directionTir.y + transform.right * directionTir.x) * projectileSpeed;
            angulSpeed += 2;
        }
        else
        {
            Launch();
        }


    }
    #endregion




}
