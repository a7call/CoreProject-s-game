using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotZone : Enemy
{
    [SerializeField] protected EnemyScriptableObject enemyScriptableObject;
    [SerializeField] private LayerMask hitLayer;
    private bool hasStartAttacking = true;

    protected override void Start()
    {
        // Set data
        base.Start();
        SetData();
        aIPath.slowdownDistance = aIPath.endReachedDistance;
        SetMaxHealth();
    }

    //  Corriger existe en deux examplaire

    protected override void  SetData()
    {
        aIPath.maxSpeed = Random.Range(enemyScriptableObject.moveSpeed, enemyScriptableObject.moveSpeed + 1);
        maxHealth = enemyScriptableObject.maxHealth;
        attackRange = enemyScriptableObject.attackRange;
    }
    protected override void Update()
    {
        base.Update();
        //getRota();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                //StartCoroutine(Zone());
                // suit le path créé et s'arrête pour tirer

                break;


        }

    }
  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
    }



    
}
