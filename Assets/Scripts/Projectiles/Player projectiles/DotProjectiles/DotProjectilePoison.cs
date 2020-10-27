using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProjectilePoison : PlayerProjectiles
{
    protected bool isPoisoned;
    [SerializeField] protected float timeBetweenHits;
    [SerializeField] protected float poisonedTimer;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy == null) Destroy(gameObject);
            enemy.TakeDamage(weaponDamage);
            StartCoroutine(DotTimer());
            StartCoroutine(DotCo(enemy));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            speed = 0;
           
        }

    }

    private IEnumerator DotCo(Enemy enemy)
    {
        while (isPoisoned && enemy != null)
        {   
            yield return new WaitForSeconds(timeBetweenHits);
            if (enemy != null)
            {
                enemy.TakeDamage(weaponDamage);
            }
            else
            {
                Destroy(gameObject);
            }

              
        }
      
        

    }
    private IEnumerator DotTimer()
    {
        isPoisoned = true;
        yield return new WaitForSeconds(poisonedTimer);
        isPoisoned = false;
        Destroy(gameObject);

    }
}
