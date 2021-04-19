using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotZone : Enemy
{
    [SerializeField] protected EnemyScriptableObject enemyScriptableObject;
    [SerializeField] private LayerMask hitLayer;
    private bool hasStartAttacking = true;

    protected override void Start()
    {
        // Set data
        base.Start();
        SetData();
        SetMaxHealth();
    }

    //  Corriger existe en deux examplaire

    void SetData()
    {
        aIPath.maxSpeed = Random.Range(enemyScriptableObject.moveSpeed, enemyScriptableObject.moveSpeed + 1);
        maxHealth = enemyScriptableObject.maxHealth;
        whiteMat = enemyScriptableObject.whiteMat;
        defaultMat = enemyScriptableObject.defaultMat;
        attackRange = enemyScriptableObject.attackRange;
    }
    protected override void Update()
    {
        base.Update();
        //getRota();
               switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                StartCoroutine(Zone());
                // suit le path créé et s'arrête pour tirer

                break;


        }

    }
    private IEnumerator Zone()
    {
        if (hasStartAttacking)
        {
            hasStartAttacking = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, hitLayer);

            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<Player>().TakeDamage(1);

            }
            yield return new WaitForSeconds(1f);
            hasStartAttacking = true;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
    }

    public float RotateSpeed;
    public Quaternion trun;
    void getRota()
    {
            trun = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(aIPath.desiredVelocity.x, aIPath.desiredVelocity.y, 0 )), Time.deltaTime * RotateSpeed);
    }

    public GameObject zone;

    bool isDOne;


    void test()
    {
        if (!isDOne)
        {

           GameObject tureee =  Instantiate(zone, transform.position, transform.rotation);
            float angle = Mathf.Atan2(aIPath.desiredVelocity.y, aIPath.desiredVelocity.x) * Mathf.Rad2Deg;
            tureee.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("tes21");

        if (collision.CompareTag("Finish"))
        {
            isDOne = true;
            print("tes");
        }
        else
        {
            isDOne = false;
        }
    }
}
