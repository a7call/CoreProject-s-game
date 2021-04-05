using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivesModule : ModuleLauchPhase
{
    [SerializeField] protected float timeBeforDesactivation;
    [SerializeField] protected float radius;
    [SerializeField] protected float explosionDamage;
    [SerializeField] protected LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;


    protected override void Start()
    {
         if (PlayerProjectiles.isNuclearExplosionModule || PlayerProjectiles.isAtomiqueExplosionModule)
            {
                explosionDamage *= PlayerProjectiles.explosionDamageMultiplier;
            }
         base.Start();
    }
    protected virtual IEnumerator ExplosionOnEnemy()
    {
        yield return new WaitForSeconds(timeBeforDesactivation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                ExplosionEffects(enemy);
            }
            if (hit.gameObject.GetComponent<Player>())
            {
                Player player = hit.gameObject.GetComponent<Player>();
                player.TakeDamage(1);
            }
            
        }
        Destroy(gameObject);
    }
    protected virtual void ExplosionEffects(Enemy enemy)
    {
        Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
        CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, Direction, knockBackTime, enemy));
        if (PlayerProjectiles.isNuclearExplosionModule)
        {
            CoroutineManager.Instance.StartCoroutine(NuclearExplosionModule.NuclearDotCo(enemy));
        }
        enemy.TakeDamage(explosionDamage);
        
    }
}
