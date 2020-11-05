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
    protected GameObject player;
    protected Vector2 dir;
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
        base.Update();
        GetAttackDirection();
        dir = (attackPoint.position - player.transform.position).normalized;
        if (Input.GetMouseButtonDown(0))
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
    }

    protected virtual IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

            foreach (Collider2D enemy in enemyHit)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
                print(dir);
                enemy.GetComponent<Rigidbody2D>().AddForce(dir * knockBackForce * Time.deltaTime);
            }

            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
        
    }
}
