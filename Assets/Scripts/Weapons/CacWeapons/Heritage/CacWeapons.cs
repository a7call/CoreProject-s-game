using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Classe héritière de Weapons.cs et mère de toutes les armes CAC.
/// Elle contient une fonction permettant de recupérer la direction de l'attaque
/// </summary>
public class CacWeapons : Weapons, IPlayerWeapon
{

    [SerializeField] protected CaCWeaponScriptableObject CacWeaponData;

    public WeaponScriptableObject WeaponData {
        get
        {
            return CacWeaponData;
        }
     }
    protected float knockBackForce;
    protected float knockBackTime;
    protected Vector3 dir;
    protected float attackRadius;
    public float attackRange;

    //Vampirisme
    [HideInInspector]
    public static bool isVampirismeModule;

    //FuryModule
    [HideInInspector]
    protected bool CadenceAlreadyUp = false;
    [HideInInspector]
    public static bool isFuryModule;
    [HideInInspector]
    public static int CadenceMultiplier;

    //UpRangeModule
    [HideInInspector]
    protected bool isRangeAlreadyUp = false;
    [HideInInspector]
    public static bool isUpRangeCacModule;
    [HideInInspector]
    public static float RangeMultiplier;

    [SerializeField] protected string AttackSound;


    protected override void Awake()
    {
        SetData();
        base.Awake();
      

    }

    private void Start()
    {
        SetStatDatas();
    }
    protected override void Update()
    {
        base.Update();
        GetKnockBackDir();
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }

    #region Datas
    private void SetData()
    {
        enemyLayer = WeaponData.enemyLayer;
        image = WeaponData.image;
    }

    private void SetStatDatas()
    {
        
        damage = player.damage.Value;
        knockBackForce = player.knockBackForce.Value;
        attackDelay = player.attackSpeed.Value;
        knockBackTime = WeaponData.knockBackTime;
        attackRadius = player.attackRadius.Value;
        attackRange = player.attackRange.Value;
    }
    #endregion


    #region Attack

    public GameObject SlashObj;

    protected virtual IEnumerator Attack()
    {
        if (!PauseMenu.isGamePaused)
        {
            PlayEffectSound(AttackSound);
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
            AttackAppliedOnEnemy(enemyHit);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }


    protected virtual void AttackAppliedOnEnemy(Collider2D [] enemyHit)
    {
        foreach (Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackForce, dir, knockBackTime, enemyScript));
            }
            if (enemy.gameObject.CompareTag("EnemyProjectil"))
                Destroy(enemy.gameObject);

        }
           
    }
   // function to trigger the attack +  "animation" (flip)
   // Slash Object Instantiate   
    public void ToAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
            GameObject obj = Instantiate(SlashObj, attackPoint.position, transform.rotation);
            obj.transform.parent = transform;
            StartCoroutine(Attack());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    private void GetKnockBackDir()
    {
        dir = (attackPoint.position - player.transform.position).normalized;
    }

    #endregion
}
