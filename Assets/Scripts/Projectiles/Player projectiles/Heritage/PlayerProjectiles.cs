using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerProjectiles : MonoBehaviour
{

    // ExplosionModule
    [HideInInspector]
    public static bool isExplosiveAmo = false; 
    [HideInInspector]
    public static bool isImolationModule = false;
    [HideInInspector]
    public static float explosiveRadius;
    [HideInInspector]
    public static LayerMask hitLayer;
    [HideInInspector]
    public static int explosionDamage;




    protected GameObject player;
    protected GameObject weapon;
    protected Vector3 dir;
    protected Transform playerTransform;
    protected float speed;
    protected Weapons weaponAttackP;
    protected LayerMask weaponLayer;
    protected int weaponDamage;
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
            if (isExplosiveAmo)
            {
                ExplosiveAmoModule.explosionFnc(this.gameObject);
            }
            if (isImolationModule)
            {
               CoroutineManager.Instance.StartCoroutine(ImmolationModule.ImolationDotCo(enemy));
            }
            Destroy(gameObject);
           
        }
 
    }
    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(Dispersion, Vector3.forward) * dir;
    }
}
