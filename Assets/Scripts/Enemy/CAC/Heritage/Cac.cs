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
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    #region State Management
    protected override void isInRange()
    {

        if (Vector3.Distance(transform.position, target.position) < attackRange  )
        {
            currentState = State.Attacking;
            AIMouvement.ShouldMove = false;
        }
        else if (currentState != State.Chasing && !isAttacking )
        {
            currentState = State.Chasing;
            AIMouvement.ShouldMove = true;

        }

    }
    #endregion

    #region Datas
    [SerializeField] protected CacScriptableObject CacDatas;
    protected override void SetData()
    {
        attackRange = CacDatas.attackRange;
        attackRadius = CacDatas.attackRadius;
        hitLayers = CacDatas.hitLayers;

        AIMouvement.Speed = Random.Range(CacDatas.moveSpeed, CacDatas.moveSpeed + 1f);

        inSight = CacDatas.InSight;
        MaxHealth = CacDatas.maxHealth;
        attackDelay = CacDatas.attackDelay;
        isSupposedToMoveAttacking = CacDatas.isSupposedToMoveAttacking;
    }
    #endregion


    #region Attack Code
    // Rayon d'attaque de l'ennemi
    private float attackRadius;
    public float AttackRadius { 
        get
        {
            return attackRadius;
        }
    }
    // Layers subissant l'attaque de l'ennemi
    private LayerMask hitLayers;
    public LayerMask HitLayers
    {
        get
        {
            return hitLayers;
        }
    }

    protected virtual IEnumerator BaseAttack()
    {
        var attackDir = target.position - transform.position;
        var attackPoint = new Vector3(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
        ApplyDamage(attackPoint);
        isAttacking = false;
        yield return new WaitForSeconds(attackDelay);
        attackAnimationPlaying = false;
    }

    protected void ApplyDamage(Vector2 pos)
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().TakeDamage(1);
            }

        }
    }
    #endregion


    #region Animation
    protected override void GetLastDirection()
    {
        if (currentState != State.Attacking)
        {
            base.GetLastDirection();
        }
    }
    #endregion
   
}
