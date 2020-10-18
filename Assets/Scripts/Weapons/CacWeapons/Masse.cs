using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///Script de l'attaque avec un zone d'impact (cercle)
/// </summary>
public class Masse : CacWeapons
{
   
    private void Update()
    {
        GetAttackDirection();
    }


    // Dégats à tous les ennemis présents dans zone 
    void AttackCACZone()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);


        foreach (Collider2D enemy in enemyHit)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

    }

    protected override void GetAttackDirection()
    {
        base.GetAttackDirection();
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }
}
