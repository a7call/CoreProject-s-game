using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAlienProj : TraversProjectile
{
    [SerializeField] protected float Timer;
    [SerializeField] protected float KryptoDamage;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, directionTir, knockBackTime, enemy));
            CoroutineManager.Instance.StartCoroutine(KryptoHit(enemy));
            //Modules
            ModuleProcs(enemy);

            nbTravers++;
            if (nbTravers >= nbTraversAuthorized)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.layer == 10) Destroy(gameObject);

    }

    protected IEnumerator KryptoHit(Enemy enemy)
    {
       while (enemy)
       {
            yield return new WaitForSeconds(Timer);
            if (enemy == null) yield break;
            enemy.TakeDamage(KryptoDamage);
       }
    }
}
