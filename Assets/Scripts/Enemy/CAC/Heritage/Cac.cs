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
    //DontFuckModule
    public static bool IsDontFuckWithMe = false;


    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    #region State Management
    protected override void isInRange()
    {

        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
        }
        else if (currentState != State.Chasing && !isAttacking )
        {
            currentState = State.Chasing;

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

        aIPath.maxSpeed = Random.Range(CacDatas.moveSpeed, CacDatas.moveSpeed + 1f);

        inSight = CacDatas.InSight;
        maxHealth = CacDatas.maxHealth;
        attackDelay = CacDatas.attackDelay;
        isSupposedToMoveAttacking = CacDatas.isSupposedToMoveAttacking;
    }
    #endregion


    #region Attack Code
    [SerializeField] protected Transform attackPoint;
    // Rayon d'attaque de l'ennemi
    public float attackRadius;
    // Layers subissant l'attaque de l'ennemi
    public LayerMask hitLayers;
    public Vector3 attackDir;

    protected virtual IEnumerator BaseAttack()
    {
        ApplyDamage();
        isAttacking = false;
        yield return new WaitForSeconds(attackDelay);
        attackAnimationPlaying = false;
    }

    void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag("Player"))
            {
                h.GetComponent<Player>().TakeDamage(1);
            }

        }
    }

    protected virtual void GetPlayerPos()
    {
        attackDir = target.position - transform.position;
        attackPoint.position = new Vector3(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
    }


    #endregion


    #region Animation
    protected override void GetLastDirection()
    {
        if (currentState != State.Attacking)
        {
            if (aIPath.desiredVelocity.x > 0.1 || aIPath.desiredVelocity.x < 0.1 || aIPath.desiredVelocity.y < 0.1 || aIPath.desiredVelocity.y > 0.1)
            {
                animator.SetFloat("lastMoveX", targetSetter.target.position.x - gameObject.transform.position.x);
                animator.SetFloat("lastMoveY", targetSetter.target.position.y - gameObject.transform.position.y);
            }
        }
    }
    #endregion
   
}
