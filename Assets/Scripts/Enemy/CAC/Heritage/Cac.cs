using System.Collections;
using UnityEngine;



/// <summary>
/// Classe mère des Cac et fille de Enemy.cs
/// Elle contient la methode SetData qui permet de recuperer les datas du scriptable object
/// Une fontion permettant de savoir si l'ennemi est en range pour lancer une attaque
/// Une fonction d'attaque de base 
/// Une fonction récupérant la position du joueur
/// On rappelle les fonctions issues de l'héritage
/// </summary>



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

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
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



    // Check if PLayer is in Range pour définir la State en Attacking ou Chasing (ici, pas de patrouille)
    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
        }
        else
        {
            currentState = State.Chasing;
        }
    }


    // CAC attack (TK enable or disable)
    protected virtual void BaseAttack()
    {
        rb.velocity = Vector2.zero;
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
