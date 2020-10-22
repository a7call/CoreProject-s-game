using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrikSword : CacWeapons
{
    [SerializeField] protected float electrificationRadius;
    protected void Start()
    {

    }
    protected override IEnumerator Attack()
    {

        if (!isAttacking)
        {
            isAttacking = true;
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);


            foreach (Collider2D enemy in enemyHit)
            {
                Enemy enemyH = enemy.GetComponent<Enemy>();
                enemyH.TakeDamage(damage);
                DamageToNearEnemyElectrified(enemy);

            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
    }
    private void DamageToNearEnemyElectrified(Collider2D enemy)
    {


        Collider2D[] enemyElectrified = Physics2D.OverlapCircleAll(enemy.transform.position, electrificationRadius, enemyLayer);


        foreach (Collider2D enemyE in enemyElectrified)
        {
            Enemy enemyH = enemyE.GetComponent<Enemy>();
            enemyH.TakeDamage(damage);
            DamageToNearEnemyElectrified(enemyE);
        }
    }
}
