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
            CoroutineManager.Instance.StartCoroutine(DotTimer());
            CoroutineManager.Instance.StartCoroutine(DotCo(enemy));
        }
        base.OnTriggerEnter2D(collision);

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
