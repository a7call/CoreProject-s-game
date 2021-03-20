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
    [SerializeField] protected CacScriptableObject CacDatas;
    public Vector3 attackDir;

    //DontFuckModule
    public static bool IsDontFuckWithMe = false;
    protected float attackDelay;

    protected override void Awake()
    {
        base.Awake();
        SetData();

    }

    protected virtual void Start()
    {
        SetMaxHealth();
    }

    // Set data du scriptable object Type1Data
    protected virtual void SetData()
    {
        attackRange = CacDatas.attackRange;
        attackRadius = CacDatas.attackRadius;
        hitLayers = CacDatas.hitLayers;

        aIPath.maxSpeed = Random.Range(CacDatas.moveSpeed, CacDatas.moveSpeed +1f);


        maxHealth = CacDatas.maxHealth;
        whiteMat = CacDatas.whiteMat;
        defaultMat = CacDatas.defaultMat;
        timeToSwitch = CacDatas.timeToSwich;
        attackDelay = CacDatas.attackDelay;
    }


    // Centre du rayon de l'attaque de l'ennemi
    [SerializeField] protected Transform attackPoint;
    // Rayon d'attaque de l'ennemi
    public float attackRadius;
    // Layers subissant l'attaque de l'ennemi
    public LayerMask hitLayers;
    // Check si l'ennemi est en range d'attaque
    protected bool isInAttackRange;
    protected bool isAttacking;

    // CAC attack (TK enable or disable)
    protected virtual IEnumerator BaseAttack()
    {
        if (isreadyToAttack)
        {
            animator.SetTrigger("isAttacking");
            isreadyToAttack = false;
            //ApplyDamage();
            yield return new WaitForSeconds(attackDelay);
            isreadyToAttack = true;
        }
    }

    void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag("Player"))
            {
                h.GetComponent<PlayerHealth>().TakeDamage(1);
            }

        }
    }
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

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Get the player postion at all time
    protected virtual void GetPlayerPos()
    {
        attackDir = target.position - transform.position;
        attackPoint.position = new Vector3(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
    }

    protected override void SetAnimationVariable()
    {

        if (aIPath.canMove)
        {
            animator.SetFloat("HorizontalSpeed", aIPath.velocity.x);
            animator.SetFloat("VerticalSpeed", aIPath.velocity.y);
            float EnemySpeed = aIPath.velocity.sqrMagnitude;
            animator.SetFloat("Speed", EnemySpeed);
        }
        else
        {

            animator.SetFloat("HorizontalSpeed", 0);
            animator.SetFloat("VerticalSpeed", 0);
            float EnemySpeed = 0;
            animator.SetFloat("Speed", EnemySpeed);
        }

        //mettre d'autres conditions 
      

        if (currentState == State.KnockedBack)
        {
            //animator.SetBool("isTakingDamage", true);
        }
        else
        {
            //animator.SetBool("isTakingDamage", false);
        }
    }

}
