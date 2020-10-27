using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneProjectileBouleFeu : PlayerProjectiles
{
    private bool isActive;
    [SerializeField] protected float zoneRadius;
    [SerializeField] protected float hitTimer;
    [SerializeField] protected float zoneTimer;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null) Destroy(gameObject);
            enemy.TakeDamage(weaponDamage);
            StartCoroutine(ActiveZone());
            StartCoroutine(ZoneCo());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            speed = 0;

        }

    }


    private IEnumerator ZoneCo()
    {
        while (isActive)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, zoneRadius, weaponLayer);

            yield return new WaitForSeconds(hitTimer);
            foreach (Collider2D enemyCol in enemies)
            {
                Enemy enemy  = enemyCol.GetComponent<Enemy>();
                enemy.TakeDamage(weaponDamage);
            }
           
        }
    }

    private IEnumerator ActiveZone()
    {
        isActive = true;
        yield return new WaitForSeconds(zoneTimer);
        isActive = false;
    }
}
