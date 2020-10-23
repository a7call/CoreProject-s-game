using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taser : CacWeapons
{
    [SerializeField] protected float paralysedTime;
    void Start()
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
                StartCoroutine(TasedEnemy(enemyH));

            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }
    }


    protected IEnumerator TasedEnemy(Enemy enemy)
    {
        enemy.currentState = Enemy.State.Paralysed;
        yield return new WaitForSeconds(paralysedTime);
        enemy.currentState = Enemy.State.Chasing;
    }
}
