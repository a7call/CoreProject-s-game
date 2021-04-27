using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpeeFeu : CacWeapons
{

    [SerializeField] protected float Timer;
    [SerializeField] protected int NbDegats;
    [SerializeField] protected float FeuDamage;
    

    protected override void AttackAppliedOnEnemy(Collider2D[] enemyHit)
    {


        foreach (Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage(damage);
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackForce, dir, knockBackTime, enemyScript));

               

            }
        }
    }
 
}
