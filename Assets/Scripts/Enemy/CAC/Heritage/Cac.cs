using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cac : Enemy
{
    [SerializeField] protected Type1ScriptableObject Type1Data;
    protected virtual void SetData()
    {
        attackRange = Type1Data.attackRange;
        attackRadius = Type1Data.attackRadius;
        hitLayers = Type1Data.hitLayers;

        moveSpeed = Type1Data.moveSpeed;
        aggroDistance = Type1Data.aggroDistance;

        maxHealth = Type1Data.maxHealth;
        whiteMat = Type1Data.whiteMat;
        defaultMat = Type1Data.defaultMat;
    }
    

    // Mouvement

    protected override void Aggro()
    {
        base.Aggro();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }
    protected override void Patrol()
    {
        base.Patrol();
    }



    // Health
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }

    protected float attackRange;
    [SerializeField] protected Transform attackPoint;
    protected float attackRadius;
    protected LayerMask hitLayers;



    //Attack

    // Check if PLayer is in Range
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            //BaseAttack()
        }
    }


    // CAC attack (TK enable or disable)
    protected virtual void BaseAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {
            // TakeDamage();
        }
    }


    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Get the player postion at all time
    protected virtual void GetPlayerPos()
    {
        Vector2 attackDir = target.position - transform.position;
        attackPoint.position = new Vector2(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
    }


    
}
