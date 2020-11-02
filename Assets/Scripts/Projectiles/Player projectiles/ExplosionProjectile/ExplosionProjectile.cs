using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : PlayerProjectiles
{
    [SerializeField] protected float explosionRadius;
    public static bool isNuclearExplosionModule;
    public static int explosionDamageMultiplier;
    
    private void Start()
    {
        if (isNuclearExplosionModule)
        {
            weaponDamage *= explosionDamageMultiplier;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

     Collider2D[] ennemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, weaponLayer);

        foreach(Collider2D enemy in ennemies)
        {
            Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(weaponDamage);
            speed = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (isNuclearExplosionModule)
            {
                StartCoroutine(NuclearDotCo(enemyScript));
            }


        }
         
        if (collision.CompareTag("Player") || collision.CompareTag("WeaponManager")) return;
        if(!isNuclearExplosionModule) Destroy(gameObject);
        if(ennemies == null)
        {
            Destroy(gameObject);
        }

    }

    
}
