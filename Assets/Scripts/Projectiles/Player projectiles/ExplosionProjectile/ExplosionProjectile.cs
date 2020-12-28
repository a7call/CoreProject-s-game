using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;

    private void Start()
    {
        if (isNuclearExplosionModule || isAtomiqueExplosionModule)
        {
            weaponDamage *= explosionDamageMultiplier;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);
        Explosion(ennemies);
        base.OnTriggerEnter2D(collision);
    }

    protected void Explosion(Collider2D[] ennemies)
    {
        foreach (Collider2D enemy in ennemies)
        {
            if (enemy == null) continue;
            Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
            if (enemyScript == null) continue;
            enemyScript.TakeDamage(weaponDamage);
            if (isNuclearExplosionModule)
            {
                CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemyScript));
            }
        }
    }
}
