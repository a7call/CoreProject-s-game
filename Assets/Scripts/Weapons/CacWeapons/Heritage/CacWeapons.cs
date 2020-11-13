using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    protected Vector2 dir;

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
        player = GameObject.FindGameObjectWithTag("Player");
        SetData();
       
    }
    protected override void Update()
    {
        if(isAntiEmeuteModule && !alreadyMultiplied)
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
        dir = (attackPoint.position - player.transform.position).normalized;
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
    }

    protected virtual IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

            if (isVampirismeModule)
            {
                RewardSpawner.isAttackCAC = true;
            }

            foreach (Collider2D enemy in enemyHit)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                enemyScript.rb.isKinematic = false;
                enemyScript.rb.AddForce(dir * knockBackForce);
                StartCoroutine(KnockCo(enemyScript));
            }

            yield return new WaitForSeconds(attackDelay);
            RewardSpawner.isAttackCAC = false;
            isAttacking = false;
        }
        
    }

    

    private IEnumerator KnockCo(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.currentState = Enemy.State.KnockedBack;
            yield return new WaitForSeconds(knockBackTime);
            if (enemy == null) yield break;
            enemy.currentState = Enemy.State.Attacking;
            if (enemy == null) yield break;
            enemy.rb.isKinematic = false;
        }

    }
}
