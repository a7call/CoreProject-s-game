using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagerLF : ColliderManager
{
   [SerializeField] protected float timeBetweenTicks;

    protected override IEnumerator DamageCO()
    {
        readyToHit = false;

        for (int Cont = 0; Cont < Targets.Count; Cont++)
        {
            Enemy enemy = Targets[Cont].GetComponent<Enemy>();
            enemy.TakeDamage(1);
            StartCoroutine(enemy.DotOnEnemy());
            StartCoroutine(DotCO(enemy));
        }
        yield return new WaitForSeconds(damageTimer);
        readyToHit = true;
    }

    protected IEnumerator DotCO(Enemy enemy)
    {
        while (enemy.isTakingDot)
        {
            yield return new WaitForSeconds(timeBetweenTicks);
            if (enemy == null) yield break;
            enemy.TakeDamage(1);
        }
    }
   
}
