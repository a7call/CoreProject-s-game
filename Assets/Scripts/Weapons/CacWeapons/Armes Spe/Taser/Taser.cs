using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taser : CacWeapons
{
    [SerializeField] protected float paralysedTime;


    protected override IEnumerator Attack()
    {

        if (!isAttacking)
        {
            isAttacking = true;
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

            AttackAppliedOnEnemy(enemyHit);
            foreach (Collider2D enemy in enemyHit)
            {
                Enemy enemyH = enemy.GetComponent<Enemy>();
                StartCoroutine(TasedEnemy(enemyH));

            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
    }


    protected IEnumerator TasedEnemy(Enemy enemy)
    {
        float baseMoveSpeed = enemy.AIMouvement.MoveForce;
        enemy.AIMouvement.MoveForce = 0;
        yield return new WaitForSeconds(paralysedTime);
        enemy.AIMouvement.MoveForce = baseMoveSpeed;
    }
}
