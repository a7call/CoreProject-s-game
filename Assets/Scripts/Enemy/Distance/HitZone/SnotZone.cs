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

    protected void Start()
    {
        // Set data
        SetData();
        SetMaxHealth();
    }

    //  Corriger existe en deux examplaire

    void SetData()
    {
        aIPath.maxSpeed = Random.Range(enemyScriptableObject.moveSpeed, enemyScriptableObject.moveSpeed + 1);
        maxHealth = enemyScriptableObject.maxHealth;
        whiteMat = enemyScriptableObject.whiteMat;
        defaultMat = enemyScriptableObject.defaultMat;
        attackRange = enemyScriptableObject.attackRange;
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                StartCoroutine(Zone());
                // suit le path créé et s'arrête pour tirer

                break;


        }

    }
    private IEnumerator Zone()
    {
        if (hasStartAttacking)
        {
            hasStartAttacking = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, hitLayer);

            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<Player>().TakeDamage(1);

            }
            yield return new WaitForSeconds(1f);
            hasStartAttacking = true;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
    }

}
