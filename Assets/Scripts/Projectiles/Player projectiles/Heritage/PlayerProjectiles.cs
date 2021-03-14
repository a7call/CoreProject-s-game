using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerProjectiles : MonoBehaviour
{
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


    //Knoclback
    [HideInInspector]
     public float knockBackForce;
    [HideInInspector]
    public float knockBackTime;





    protected GameObject player;
    protected GameObject weapon;
    protected Vector3 dir;
    protected Transform playerTransform;
    protected float speed;
    protected Weapons weaponAttackP;
    protected LayerMask weaponLayer;
    protected float weaponDamage;
    [SerializeField]
    protected PlayerProjectileScriptableObject PlayerProjectileData;
    protected Vector3 directionTir;
    [HideInInspector]
    public float Dispersion;
    private Rigidbody2D projectileRB;

    protected Vector3 screenMousePos;
    protected Vector3 screenPlayerPos;
    protected Vector3 screenArmePos;

    protected float RangeChangementTir = 100;

    protected virtual void Awake()
    {
        
        
        SetData();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("WeaponManager");
        weaponAttackP = weapon.transform.GetComponentInChildren<Weapons>();
        projectileRB = GetComponent<Rigidbody2D>();
        weaponDamage = weaponAttackP.damage;
        weaponLayer = weaponAttackP.enemyLayer;
        playerTransform = player.GetComponent<Transform>();

        // position de la souris sur l'écran 
        screenMousePos = Input.mousePosition;
        // position du player en pixel sur l'écran 
        screenPlayerPos = Camera.main.WorldToScreenPoint(player.transform.position);
        screenArmePos = Camera.main.WorldToScreenPoint(transform.position);
        // position du point d'attaque
        
        float distSP = new Vector3((screenMousePos - screenPlayerPos).x - player.transform.position.x, (screenMousePos - screenPlayerPos).y - player.transform.position.y).magnitude;

        if(distSP < 15)
        {
            dir = new Vector3(weaponAttackP.attackPoint.transform.position.x - weaponAttackP.transform.position.x, weaponAttackP.attackPoint.transform.position.y - weaponAttackP.transform.position.y);

        }
        else if (distSP < RangeChangementTir && distSP > 15)
        {
            dir = new Vector3((screenMousePos - screenPlayerPos).x - player.transform.position.x, (screenMousePos - screenPlayerPos).y - player.transform.position.y).normalized;
            //dir = new Vector3(weaponAttackP.attackPoint.transform.position.x - weaponAttackP.transform.position.x, weaponAttackP.attackPoint.transform.position.y - weaponAttackP.transform.position.y);

        }
        else
        {
            
            dir = new Vector3((screenMousePos - screenArmePos).x - weaponAttackP.attackPoint.position.x, (screenMousePos - screenArmePos).y - weaponAttackP.attackPoint.position.y).normalized;

        }
        ConeShoot();
    }
    

    protected virtual void Update()
    {
        
        if (isInteligentAmmoModule)
        {
            getNewDir(this.gameObject);
        }
        else
        {
            Launch();
        }

        if (isRocketAmmoModule && !AmmoSpeedAlreadyUp)
        {
            AmmoSpeedAlreadyUp = true;
            speed *= SpeedMultiplier;
        }

       
    }


    protected virtual void Launch()
    {

        transform.Translate(directionTir * speed * Time.deltaTime, Space.World) ;
        
    }
    void SetData()
    {
        speed = PlayerProjectileData.speed;
        knockBackForce = PlayerProjectileData.knockBackForce;
        knockBackTime = PlayerProjectileData.knockBackTime;
    }


 
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, dir, knockBackTime, enemy));
            //Modules
            ModuleProcs(enemy);
        }
        if (collision.gameObject.layer == 10) Destroy(gameObject);
    

    }

     protected virtual void ModuleProcs(Enemy enemy)
    {
        if (isExplosiveAmo)
        {
            ExplosiveAmoModule.explosionFnc(this.gameObject);
        }
        if (isNanoRobotModule)
        {
            NanoRobotModule.enemiesTouched.Add(enemy);
        }
        if (isImolationModule)
        {
            CoroutineManager.Instance.StartCoroutine(ImmolationModule.ImolationDotCo(enemy));
        }
        if (isCryoModule)
        {
            CoroutineManager.Instance.StartCoroutine(CryogenisationModule.CryoCo(enemy));
        }
        if (isParaModule)
        {
            CoroutineManager.Instance.StartCoroutine(ParalysieModule.ParaCo(enemy));
        }
        if (!gameObject.CompareTag("TraversProj")) Destroy(gameObject);

    }


    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(Dispersion, Vector3.forward) * dir;
    }

    
    //IntelligentAmoModule
    private float angulSpeed = 200f;
    private GameObject lockEnemy;
    public  void getNewDir(GameObject proj)
    {
        if (InteligentAmmoModule.LockEnemy(proj) != null) lockEnemy = InteligentAmmoModule.LockEnemy(proj);
        if (lockEnemy != null )
        {
            if (lockEnemy == null) return;
            Vector2 direction = (lockEnemy.transform.position - proj.transform.position);
            direction.Normalize();
            float rotationAmount = Vector3.Cross(direction, (transform.up * directionTir.y + transform.right * directionTir.x)).z;
            projectileRB.angularVelocity = -rotationAmount * angulSpeed;
            projectileRB.velocity = (transform.up * directionTir.y + transform.right * directionTir.x) * speed;
            angulSpeed += 2;
        }
        else
        {
            Launch();
        }


    }
}
