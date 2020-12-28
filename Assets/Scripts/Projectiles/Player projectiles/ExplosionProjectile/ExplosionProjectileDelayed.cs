using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileDelayed : ExplosionProjectile
{
    [SerializeField] protected float explosionDelay;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);
        CoroutineManager.Instance.StartCoroutine(DelayedExplosion(ennemies));
        Destroy(gameObject);

    }

    private IEnumerator DelayedExplosion(Collider2D[] ennemies)
    {
        yield return new WaitForSeconds(explosionDelay);
        Explosion(ennemies);
    }
}
