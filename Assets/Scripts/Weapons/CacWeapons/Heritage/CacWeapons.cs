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

    public static bool isAntiEmeuteModule;
    public static float knockBackForceMultiplier;
    private bool alreadyMultiplied;
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


    protected override void Awake()
    {
        base.Awake();
        SetData();

    }

    private void Start()
    {
        SetStatDatas();
    }
    protected override void Update()
    {
        if (isAntiEmeuteModule && !alreadyMultiplied)
        {
            knockBackForce *= knockBackForceMultiplier;
            alreadyMultiplied = true;
        }

        if (isFuryModule && !CadenceAlreadyUp)
        {
            CadenceAlreadyUp = true;
            attackDelay /= CadenceMultiplier;
        }

        if (isUpRangeCacModule && !isRangeAlreadyUp)
        {
            isRangeAlreadyUp = true;
            attackRadius *= RangeMultiplier;
        }


        base.Update();
        //GetAttackDirection();
        GetKnockBackDir();

    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }

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
        print(player.attackRadius.Value);
        attackRadius = player.attackRadius.Value;
    }

    protected virtual IEnumerator Attack()
    {
        if (!PauseMenu.isGamePaused)
        {
            
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
           
            if (isVampirismeModule)
            {
                RewardSpawner.isAttackCAC = true;
            }

            AttackAppliedOnEnemy(enemyHit);
            yield return new WaitForSeconds(attackDelay);
            RewardSpawner.isAttackCAC = false;
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
                if (PlayerProjectiles.isImolationModule)
                {
                    CoroutineManager.Instance.StartCoroutine(ImmolationModule.ImolationDotCo(enemyScript));
                }
                if (PlayerProjectiles.isCryoModule)
                {
                    CoroutineManager.Instance.StartCoroutine(CryogenisationModule.CryoCo(enemyScript));
                }
                if (PlayerProjectiles.isParaModule)
                {
                    CoroutineManager.Instance.StartCoroutine(ParalysieModule.ParaCo(enemyScript));
                }
            }
        }
           
    }

    public void ToAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("isAttacking");
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
}
