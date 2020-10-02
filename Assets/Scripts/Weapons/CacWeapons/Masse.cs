using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masse : CacWeapons
{
    [SerializeField]
    protected float attackRadius;
    private void Update()
    {
        GetAttackDirection();
    }
    void AttackCACZone()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);


        foreach (Collider2D enemy in enemyHit)
        {
            // Script de vie de l'enemi
        }

    }
}
