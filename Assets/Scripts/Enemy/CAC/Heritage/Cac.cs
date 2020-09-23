using System.Collections;
using UnityEngine;

public class Cac : Enemy
{
    [SerializeField] protected Type1ScriptableObject Type1Data;
   

   // Set data du scriptable object Type1Data
    protected virtual void SetData()
    {
        attackRange = Type1Data.attackRange;
        attackRadius = Type1Data.attackRadius;
        hitLayers = Type1Data.hitLayers;

        moveSpeed = Type1Data.moveSpeed;
        inSight = Type1Data.aggroDistance;

        maxHealth = Type1Data.maxHealth;
        whiteMat = Type1Data.whiteMat;
        defaultMat = Type1Data.defaultMat;
    }
    

    // Mouvement

    // Voir Enemy.cs (héritage)
    protected override void Aggro()
    {
        base.Aggro();
    }

    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }
    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
    }



    // Health

    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }


    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }


    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }

    //Attack

    // Distance d'où l'ennemi peu lancer une attaque
    protected float attackRange;
    // Centre du rayon de l'attaque de l'ennemi
    [SerializeField] protected Transform attackPoint;
    // Rayon d'attaque de l'ennemi
    protected float attackRadius;
    // Layers subissant l'attaque de l'ennemi
    protected LayerMask hitLayers;
    // Check si l'ennemi est en range d'attaque
    protected bool isInAttackRange;



    // Check if PLayer is in Range
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            isInAttackRange = true;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isInAttackRange = false;
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
