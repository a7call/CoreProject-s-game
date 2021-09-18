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



public abstract class Cac : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        SetData();
    }

    #region Datas
    public override IMonsterData GetMonsterData()
    {
        return CacDatas;
    }

    [SerializeField] public CacScriptableObject CacDatas;

    protected override void SetData()
    {
        attackRange = CacDatas.attackRange;
        knockBackForceToApply = CacDatas.knockBackForceToApply;
        AttackRadius = CacDatas.attackRadius;
        HitLayers = CacDatas.hitLayers;

        AIMouvement.MoveForce = CacDatas.moveForce;
        Difficulty = CacDatas.Difficulty;
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
    Vector2 attackPoint = Vector2.zero;
    protected virtual IEnumerator BaseAttack()
    {
        if (isReadyToAttack)
        {
            isReadyToAttack = false;
            var attackDir = target.position - transform.position;
            attackPoint = new Vector3(transform.position.x + Mathf.Clamp(attackDir.x, -1f, 1f), transform.position.y + Mathf.Clamp(attackDir.y, -1f, 1f));
            PlayAttackAnim(animator);
            yield return null;
        }
    }

    protected void ApplyDamage(float damage)
    {
        StartCoroutine(RestCo(animator));

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint, AttackRadius, HitLayers);

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
