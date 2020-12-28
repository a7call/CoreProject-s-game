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

    //DontFuckModule
    public static bool IsDontFuckWithMe = false;
    protected float attackDelay;

    // Set data du scriptable object Type1Data
    protected virtual void SetData()
    {
        attackRange = CacDatas.attackRange;
        attackRadius = CacDatas.attackRadius;
        hitLayers = CacDatas.hitLayers;

        moveSpeed = CacDatas.moveSpeed;
        inSight = CacDatas.aggroDistance;

        maxHealth = CacDatas.maxHealth;
        whiteMat = CacDatas.whiteMat;
        defaultMat = CacDatas.defaultMat;
        timeToSwitch = CacDatas.timeToSwich;
        attackDelay = CacDatas.attackDelay;
    }


    // Centre du rayon de l'attaque de l'ennemi
    [SerializeField] protected Transform attackPoint;
    // Rayon d'attaque de l'ennemi
    protected float attackRadius;
    // Layers subissant l'attaque de l'ennemi
    protected LayerMask hitLayers;
    // Check si l'ennemi est en range d'attaque
    protected bool isInAttackRange;
    protected bool isAttacking;

    // CAC attack (TK enable or disable)
    protected virtual IEnumerator BaseAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayers);

            foreach (Collider2D h in hits)
            {

                if (h.CompareTag("Player"))
                {
                    h.GetComponent<PlayerHealth>().TakeDamage(1);
                }
               
            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
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
