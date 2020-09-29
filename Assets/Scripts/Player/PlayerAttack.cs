using System.Collections;
using UnityEngine;

/// <summary>
///  Classe gérant les attaques du joueur
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    public PlayerScriptableObjectScript playerData;
    public Transform attackPoint;
    public Animator animator;
    public LayerMask enemyLayer;
    public float attackRadius;
    Vector3 screenMousePos;
    Vector3 screenPlayerPos;
    public GameObject projectil;


    // Update is called once per frame
    void Update()
    {

    }

    // récupère la direction de l'attaque lancé par le joueur

    void GetAttackDirection()
    {

         screenMousePos = Input.mousePosition;
         screenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, attackPoint.position);
    }



    [SerializeField]
    float rotationRadius = 0.5f, angularSpeed = 2f;
    float posX, posY;
    float angle = 0.3f;
    // Update is called once per frame
    private void Cleave()
    {
            posX = transform.position.x + (screenMousePos - screenPlayerPos).normalized.x * 0.3f + Mathf.Cos(angle) * rotationRadius;
            posY = transform.position.y + (screenMousePos - screenPlayerPos).normalized.y * 0.3f + Mathf.Sin(angle) * rotationRadius;
            attackPoint.position = new Vector2(posX, posY);
            
            angle = angle + Time.deltaTime * angularSpeed;

            if (angle >= 3f)
            {
            angle = 0.3f;
            }
        }
       
           
        
    }

