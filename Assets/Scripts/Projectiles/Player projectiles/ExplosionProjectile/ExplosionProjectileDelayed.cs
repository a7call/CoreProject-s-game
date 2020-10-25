using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileDelayed : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected float explosionDelay;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);
        speed = 0;
        transform.position = collision.transform.position;
        foreach (Collider2D enemy in ennemies)
        {
            StartCoroutine(DelayedExplosion(enemy));
        }

        if (collision.CompareTag("Player") || collision.CompareTag("WeaponManager")) return;
       

    }

    private IEnumerator DelayedExplosion(Collider2D enemy)
    {
        yield return new WaitForSeconds(explosionDelay);
        Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
        enemyScript.TakeDamage(weaponDamage);
        Destroy(gameObject);
    }
}
