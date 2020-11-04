using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileTeteChercheuse : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected float directionUpdateTime;
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected GameObject lockedEnemy;


    private bool isDirUpdate;
    private bool isEnemyLocked;


    protected override void Update()
    {
        base.Update();
        LockEnemy();
        if (!isDirUpdate && isEnemyLocked) StartCoroutine(UpdateDirection());

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
    private IEnumerator UpdateDirection()
    {
        isDirUpdate = true;
        dir = (lockedEnemy.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(directionUpdateTime);

    }

    private void LockEnemy()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRadius, weaponLayer);

        if (enemy.gameObject != null)
        {
            isEnemyLocked = true;
            lockedEnemy = enemy.gameObject;
        }
           
        
    }
}
