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



public class Cac : Enemy, IMonster
{
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    #region Datas
    public IMonsterData Datas {
        get
        {
            return CacDatas;
        }
    }
    [SerializeField] public CacScriptableObject CacDatas;

    protected override void SetData()
    {
        attackRange = CacDatas.attackRange;
        AttackRadius = CacDatas.attackRadius;
        HitLayers = CacDatas.hitLayers;

        AIMouvement.Speed = CacDatas.moveSpeed;
        difficulty = CacDatas.Difficulty;
        inSight = CacDatas.InSight;
        MaxHealth = CacDatas.maxHealth;
    }
    #endregion


    #region Attack Code
    // Rayon d'attaque de l'ennemi
    public float AttackRadius {
        get;
        private set;
    }
    // Layers subissant l'attaque de l'ennemi
    public LayerMask HitLayers
    {
        get;
        private set;
    }

    protected virtual IEnumerator BaseAttack()
    {
        var attackDir = target.position - transform.position;
        var attackPoint = new Vector3(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f)); ;
        ApplyDamage(attackPoint, damage: 50);
        isAttacking = false;
        yield return new WaitForSeconds(2f);
        attackAnimationPlaying = false;
    }

    protected void ApplyDamage(Vector2 pos, float damage)
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, AttackRadius, HitLayers);

        foreach (Collider2D h in hits)
        {

            if (h.CompareTag(EnemyConst.PLAYER_TAG))
            {
                h.GetComponent<Player>().TakeDamage(damage);
            }

        }
    }
    #endregion
}
