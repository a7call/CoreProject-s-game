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
        foreach (Collider2D enemy in ennemies)
        {
            CoroutineManager.Instance.StartCoroutine(DelayedExplosion(enemy));
        }

        if (collision.CompareTag("Player") || collision.CompareTag("WeaponManager")) return;
        base.OnTriggerEnter2D(collision);

    }

    private IEnumerator DelayedExplosion(Collider2D enemy)
    {
        yield return new WaitForSeconds(explosionDelay);
        Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
        enemyScript.TakeDamage(weaponDamage);
        if (isNuclearExplosionModule)
        {
            CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemyScript));
        }
    }
}
