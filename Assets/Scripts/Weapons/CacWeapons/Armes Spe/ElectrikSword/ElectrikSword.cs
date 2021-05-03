using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrikSword : CacWeapons
{
    private StunAbility stun;
    [SerializeField] float stunTime = 3f; 
    protected override void Awake()
    {
        base.Awake();
        stun = new StunAbility(stunTime);
    }
    protected override void AttackAppliedOnEnemy(Collider2D[] enemyHit)
    {
        base.AttackAppliedOnEnemy(enemyHit);
        foreach(Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                stun.ApplyEffect(enemy.GetComponent<Enemy>());
            }

        }
    }
}
