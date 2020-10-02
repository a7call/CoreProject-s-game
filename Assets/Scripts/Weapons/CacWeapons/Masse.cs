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


    // Dégats à tous les ennemis présents dans zone 
    void AttackCACZone()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);


        foreach (Collider2D enemy in enemyHit)
        {
            // Script de vie de l'enemi
        }

    }




    // héritage 
    protected override void GetAttackDirection()
    {
        base.GetAttackDirection();
    }
}
