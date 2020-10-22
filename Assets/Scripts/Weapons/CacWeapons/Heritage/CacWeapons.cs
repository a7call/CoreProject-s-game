﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Weapons.cs et mère de toutes les armes CAC.
/// Elle contient une fonction permettant de recupérer la direction de l'attaque
/// </summary>
public class CacWeapons : Weapons
{
    [SerializeField] protected WeaponScriptableObject WeaponData;
    
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }
    protected virtual void Update()
    {
        GetAttackDirection();
    }
    protected virtual void AttackCAC()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);


        foreach (Collider2D enemy in enemyHit)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }

   private void SetData()
    {
        attackRadius = WeaponData.attackRadius;
        enemyLayer = WeaponData.enemyLayer;
        damage = WeaponData.damage;
    }
}
