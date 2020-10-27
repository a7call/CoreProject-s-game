using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversProjectile : PlayerProjectiles
{
    private int nbTravers = 0;
    [SerializeField] protected int nbTraversAuthorized;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);
            nbTravers++;
            if (nbTravers >= nbTraversAuthorized) { 
                Destroy(gameObject);
            }
            
        }

    }
}
