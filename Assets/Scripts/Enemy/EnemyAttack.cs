using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected EnemyScriptableObject EnemyData;


    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected LayerMask player;
    [SerializeField] protected LayerMask enemy;

    private void Start()
    {
        SetData();
    }
    private void Update()
    {
        GetPlayerPos();
    }


    // Check if PLayer is in Range
    void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            //BaseAttack()
        }
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // CAC attack (TK enable)
    void BaseAttack()
    {
        LayerMask hitLayers = player | enemy;
       

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {
            // TakeDamage();
        }
    }

    //Get the player postion at all time
    void GetPlayerPos()
    {
        Vector2 attackDir = target.position - transform.position;
        attackPoint.position = new Vector2(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
    }

    private void SetData()
    {
        attackRange = EnemyData.attackRange;
        attackRadius = EnemyData.attackRadius;
        player = EnemyData.player;
        enemy = EnemyData.enemy;
    }
}
