using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileTeteChercheuse : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected float directionUpdateTime;
    [SerializeField] protected float detectionRadius;
    protected GameObject lockedEnemy;
    private bool isEnemyLocked;
    public Rigidbody2D rb;


    protected override void Update()
    {
        base.Update();
        LockEnemy();


    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);

        foreach (Collider2D enemy in ennemies)
        {
            Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(weaponDamage);
        }
        base.OnTriggerEnter2D(collision);


    }
    private void UpdateDirection()
    {
       
        Vector3 direction= (lockedEnemy.transform.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        Vector3.RotateTowards(transform.forward, direction, 500f * Time.deltaTime,0f);
        

    }

    protected override void Launch()
    {
        if (isEnemyLocked)
        {
            Vector2 direction = ((Vector2)lockedEnemy.transform.position - rb.position);
            direction.Normalize();
            float rotationAmount = Vector3.Cross(direction, (transform.up * directionTir.y + transform.right * directionTir.x)).z;
            rb.angularVelocity = -rotationAmount * 200f;
            rb.velocity = (transform.up*directionTir.y+transform.right*directionTir.x)* speed;
            
        }
        else
        {
           transform.Translate(directionTir * speed* Time.deltaTime, Space.World);
        }
    }

    private void LockEnemy()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRadius, weaponLayer);

        if (enemy != null)
        {
            isEnemyLocked = true;
            lockedEnemy = enemy.gameObject;
        }
    }
}
