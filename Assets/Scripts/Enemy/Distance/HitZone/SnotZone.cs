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
    StinkManager stinkManager;
    [SerializeField] private int stinkRadius;

    protected override void Start()
    {
        // Set data
        base.Start();
        stinkManager = FindObjectOfType<StinkManager>();
        SetData();
        aIPath.slowdownDistance = aIPath.endReachedDistance;
        SetMaxHealth();
    }

    //  Corriger existe en deux examplaire

    protected override void  SetData()
    {
        aIPath.maxSpeed = Random.Range(enemyScriptableObject.moveSpeed, enemyScriptableObject.moveSpeed + 1);
        MaxHealth = enemyScriptableObject.maxHealth;
        attackRange = enemyScriptableObject.attackRange;
    }
    protected override void Update()
    {
        base.Update();
        if(aIPath.velocity.sqrMagnitude >= 0.1f)
            stinkManager.SetStinkTile(transform.position, stinkRadius);
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                break;


        }

    }
  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
    }



    
}
