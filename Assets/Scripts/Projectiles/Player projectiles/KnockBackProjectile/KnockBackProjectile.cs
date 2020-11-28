using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackProjectile : PlayerProjectiles
{
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, dir, knockBackTime, enemy));
        }
        base.OnTriggerEnter2D(collision);

    }

    private IEnumerator KnockCo(Enemy enemy)
    {
        if(enemy != null)
        {
            enemy.rb.isKinematic = false;
            enemy.rb.AddForce(dir * knockBackForce);
            enemy.currentState = Enemy.State.KnockedBack;
            yield return new WaitForSeconds(knockBackTime);
            if (enemy == null) yield break;
            enemy.currentState = Enemy.State.Attacking;
            if (enemy == null) yield break;
            enemy.rb.isKinematic = true;
        }
       
    }
}
