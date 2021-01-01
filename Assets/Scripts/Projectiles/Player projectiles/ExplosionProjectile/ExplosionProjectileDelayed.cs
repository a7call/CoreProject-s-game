using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileDelayed : ExplosionProjectile
{
    [SerializeField] protected float explosionDelay;
    private Collider2D[] ennemies;

     protected override void Update()
    {
        base.Update();
        ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, dir, knockBackTime, enemy));
            //Modules
            ModuleProcs(enemy);
        }
        
        CoroutineManager.Instance.StartCoroutine(DelayedExplosion(ennemies, collision));



    }

    private IEnumerator DelayedExplosion(Collider2D[] ennemies, Collider2D collision)
    {
        yield return new WaitForSeconds(explosionDelay);
        Explosion(ennemies, collision);
    }
    protected override void Explosion(Collider2D[] ennemies, Collider2D collision)
    {
        if(ennemies.Length > 0)
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
       
}
