using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrikSword : CacWeapons
{
    [SerializeField] protected float electrificationRadius;

    //protected override IEnumerator Attack()
    //{

    //    if (!isAttacking)
    //    {
    //        isAttacking = true;
    //        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

    //        AttackAppliedOnEnemy(enemyHit);
    //        foreach (Collider2D enemy in enemyHit)
    //        {
    //            DamageToNearEnemyElectrified(enemy);
    //        }
    //        yield return new WaitForSeconds(attackDelay);
    //        isAttacking = false;
    //    }
    //}
    //private void DamageToNearEnemyElectrified(Collider2D enemy)
    //{


    //    Collider2D[] enemyElectrified = Physics2D.OverlapCircleAll(enemy.transform.position, electrificationRadius, enemyLayer);


    //    foreach (Collider2D enemyE in enemyElectrified)
    //    {
    //        if (enemyE == enemy) continue;
    //        Enemy enemyH = enemyE.GetComponent<Enemy>();
    //        enemyH.TakeDamage(damage);
    //    }
    //}
}
