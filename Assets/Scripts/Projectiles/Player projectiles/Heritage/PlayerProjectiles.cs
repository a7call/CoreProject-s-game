using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerProjectiles : MonoBehaviour
{
    public static bool isNuclearExplosionModule;
    public static bool isAtomiqueExplosionModule;
    public static int explosionDamageMultiplier;

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
    public float Dispersion;

    protected virtual void Awake()
    {
        SetData();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("WeaponManager");
        weaponAttackP = weapon.transform.GetComponentInChildren<Weapons>();
        weaponDamage = weaponAttackP.damage;
        weaponLayer = weaponAttackP.enemyLayer;
        playerTransform = player.GetComponent<Transform>();
        dir = (weaponAttackP.attackPoint.position - playerTransform.position).normalized;
        ConeShoot();
    }


    protected virtual void Update()
    {
        Launch();
        if (isInteligentAmmoModule)
        {
           CoroutineManager.Instance.StartCoroutine(getNewDir(this.gameObject));
        }

        if (isRocketAmmoModule && !AmmoSpeedAlreadyUp)
        {
            AmmoSpeedAlreadyUp = true;
            speed *= SpeedMultiplier;
        }

       
    }
   

    protected virtual void Launch()
    {
        transform.Translate(directionTir * speed * Time.deltaTime);
    }
    void SetData()
    {
        speed = PlayerProjectileData.speed;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);

            

            //Modules
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

            if (collision.CompareTag("Player") || collision.CompareTag("WeaponManager")) return;
            if(!gameObject.CompareTag("TraversProj")) Destroy(gameObject);

            

        }
 
    }
    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(Dispersion, Vector3.forward) * dir;
    }


    //IntelligentAmoModule
    public bool isDirUpdat;
    public IEnumerator getNewDir(GameObject proj)
    {
        if (InteligentAmmoModule.LockEnemy(proj) != null && !isDirUpdat)
        {

            isDirUpdat = true;
            directionTir = (InteligentAmmoModule.LockEnemy(proj).transform.position - proj.transform.position).normalized;
            yield return new WaitForSeconds(0.5f);
            isDirUpdat = false;
        }
        else
        {
            yield break;
        }


    }
}
