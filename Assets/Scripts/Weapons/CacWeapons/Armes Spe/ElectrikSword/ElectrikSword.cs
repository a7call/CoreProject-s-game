using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrikSword : CacWeapons
{
    protected override void AttackAppliedOnEnemy(Collider2D[] enemyHit)
    {
        base.AttackAppliedOnEnemy(enemyHit);
        foreach(Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {

            }

        }
    }
}
