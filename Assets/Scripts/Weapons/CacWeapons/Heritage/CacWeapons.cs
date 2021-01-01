using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Classe héritière de Weapons.cs et mère de toutes les armes CAC.
/// Elle contient une fonction permettant de recupérer la direction de l'attaque
/// </summary>
public class CacWeapons : Weapons
{

    public static bool isAntiEmeuteModule;
    public static float knockBackForceMultiplier;
    private bool alreadyMultiplied;

    [SerializeField] protected CaCWeaponScriptableObject WeaponData;
    protected float knockBackForce;
    protected float knockBackTime;
    protected GameObject player;
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

    [HideInInspector]
    public Sprite image;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        SetData();

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
        GetAttackDirection();
        GetKnockBackDir();
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Attack());
        }


    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }

    private void SetData()
    {
        attackRadius = WeaponData.attackRadius;
        enemyLayer = WeaponData.enemyLayer;
        damage = WeaponData.damage;
        knockBackForce = WeaponData.knockBackForce;
        attackDelay = WeaponData.AttackDelay;
        knockBackTime = WeaponData.knockBackTime;
        image = WeaponData.image;
    }

    protected virtual IEnumerator Attack()
    {
        if (!isAttacking && !PauseMenu.isGamePaused)
        {
            isAttacking = true;
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


    protected void AttackAppliedOnEnemy(Collider2D [] enemyHit)
    {
        foreach (Collider2D enemy in enemyHit)
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

    public void ToAttack()
    {
        CoroutineManager.Instance.StartCoroutine(Attack());
    }

    private void GetKnockBackDir()
    {
        dir = (attackPoint.position - player.transform.position).normalized;
    }
}
