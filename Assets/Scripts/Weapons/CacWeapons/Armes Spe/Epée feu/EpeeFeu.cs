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

                StartCoroutine(Feu(enemyScript));

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


    public IEnumerator Feu(Enemy enemy)
    {
        int i;
        for (i = 0; i < NbDegats; i++)
        {
            yield return new WaitForSeconds(Timer);
            if (enemy.isOnFire == false)
            {
                enemy.isOnFire = true;
                enemy.TakeDamage(FeuDamage);
            }
            
        }

        enemy.isOnFire = false;
    }
}
