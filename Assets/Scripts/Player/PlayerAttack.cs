using UnityEngine;

/// <summary>
///  Classe gérant le les attaques du joueur
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    public PlayerScriptableObjectScript playerData;
    public Transform attackPoint;
    public Animator animator;
    public LayerMask enemyLayer;
    public float attackRadius;

    public GameObject projectil;


    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();  
    }

    // récupère la direction de l'attaque lancé par le joueur
    void GetAttackDirection()
    {
        Vector3 screenMousePos = Input.mousePosition;
        Vector3 screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        attackPoint.position = new Vector2(transform.position.x + (screenMousePos - screenPlayerPos).normalized.x, transform.position.y + (screenMousePos - screenPlayerPos).normalized.y);
    }

    // récupere tous les enemis touchés par une attaque
    void AttackCAC()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach(Collider2D enemy in enemyHit)
        {
            // Script de vie de l'enemi
        }

    }

    void InstantiateProjectile()
    {
        GameObject.Instantiate(projectil);
    }

    // Gizmo de Test
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
