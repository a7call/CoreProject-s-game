using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileDelayed : ExplosionProjectile
{
    //[SerializeField] protected float explosionDelay;
    //private Collider2D[] ennemies;

    // protected  void Update()
    //{
    //    ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, WeaponLayer);
    //}
    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        Enemy enemy = collision.GetComponent<Enemy>();
    //        enemy.TakeDamage(Damage);            //Modules
    //    }
        
    //    CoroutineManager.GetInstance().StartCoroutine(DelayedExplosion(ennemies, collision));



    //}

    //private IEnumerator DelayedExplosion(Collider2D[] ennemies, Collider2D collision)
    //{
    //    yield return new WaitForSeconds(explosionDelay);
    //    Explosion(ennemies, collision);
    //}
    //protected override void Explosion(Collider2D[] ennemies, Collider2D collision)
    //{
    //    if(ennemies.Length > 0)
    //    {
    //        foreach (Collider2D enemy in ennemies)
    //        {
    //            if (enemy == null) continue;
    //            Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
    //            if (enemy == null || enemy == collision) continue;
    //            enemyScript.TakeDamage(Damage);
               
    //        }
    //    }
    //}
       
}
