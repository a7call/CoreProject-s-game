using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagerExtincteur : ColliderManager
{
    protected override IEnumerator DamageCO()
    {
        readyToHit = false;

        for (int Cont = 0; Cont < Targets.Count; Cont++)
        {
            Enemy enemy = Targets[Cont].GetComponent<Enemy>();
            enemy.TakeDamage(1);
            enemy.isSlowed = true;
        }
        yield return new WaitForSeconds(damageTimer);
        readyToHit = true;
    }
}
