using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProjectilePoison : PlayerProjectiles
{
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float duration;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
   
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(!enemy.IsPoisoned)
                 CoroutineManager.Instance.StartCoroutine(DotCo(enemy, damageAmount, duration));
        }
        base.OnTriggerEnter2D(collision);
        

    }

    private IEnumerator DotCo(Enemy enemy, float damageAmount, float duration)
    {
        enemy.IsPoisoned = true;
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            
            if (enemy == null) break;
            enemy.TakeDamage(damage);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
        enemy.IsPoisoned = false;
        
      
        

    }
}
