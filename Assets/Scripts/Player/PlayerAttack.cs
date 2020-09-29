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

    private void Start()
    {
        Cleave();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isCleaving)
        {
            AngleCalcule();
            isCleaving = true;
        }
        if(isCleaving) Cleave();
        if (!isCleaving) GetAttackDirection();
       
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
        Gizmos.DrawLine(transform.position,transform.forward);
    }



    [SerializeField]
    float rotationRadius = 0.5f, angularSpeed = 2f, rotationTime;
    float posX, posY;
    float angle;
    float startAngle;
    bool isCleaving;
    // Update is called once per frame
    private void Cleave()
    {
        posX = transform.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = transform.position.y + Mathf.Sin(angle) * rotationRadius;
        attackPoint.position = new Vector2(posX, posY);

        angle = angle + Time.deltaTime * angularSpeed;

    }
       

    private void AngleCalcule()
    {
        Vector3 dir = (attackPoint.position - transform.position).normalized;
        StartCoroutine(CleavingTime());
        Vector3 faceVector = new Vector3(0, Mathf.Abs(transform.position.y + 1), 0);
        angle =  Mathf.Deg2Rad *(Vector3.Angle( dir, faceVector));
        startAngle = angle;
        if (dir.x > 0)
        {
            angle = -angle;
        }
       
    }

    private IEnumerator CleavingTime()
    {
        yield return new WaitForSeconds(rotationTime);
        isCleaving = false;

    }


}

