﻿using System.Collections;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerAttack : MonoBehaviour
{

    public PlayerScriptableObjectScript playerData;
    public Transform attackPoint;
    public Animator animator;
    public LayerMask enemyLayer;
    public float attackRadius;
    public GameObject projectil;

    // Update is called once per frame
    void Update()
    {
       
    }


    // récupere tous les enemis touchés par une attaque
    void AttackCACZone()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        

        foreach(Collider2D enemy in enemyHit)
        {
            // Script de vie de l'enemi
        }

    }

    void AttackCACMono()
    {
        Collider2D enemyHit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        // do something

    }

    void InstantiateProjectile()
    {
        GameObject.Instantiate(projectil);
    }

    // Gizmo de Test
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,attackPoint.position);
    }


  
   

}

