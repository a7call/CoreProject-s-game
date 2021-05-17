using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;
    protected float screenShakePower = 0.4f;
    protected float screenShakeDuration = 0.3f;

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);
        Explosion(ennemies, collision);
        base.OnTriggerEnter2D(collision);
        cameraFollow.StartShake(screenShakeDuration, screenShakePower);
    }

    protected virtual void Explosion(Collider2D[] ennemies, Collider2D collision)
    {
        foreach (Collider2D enemy in ennemies)
        {
            
            Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
            if (enemyScript == null) continue;
            if (isNuclearExplosionModule)
            {
               // CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemyScript));
            }
            if (enemy == null || enemy == collision) continue;
            enemyScript.TakeDamage(damage);
        }
    }
}
