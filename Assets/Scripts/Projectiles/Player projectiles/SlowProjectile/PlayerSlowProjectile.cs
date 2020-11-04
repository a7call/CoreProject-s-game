using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowProjectile : PlayerProjectiles
{
    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(weaponDamage);
            if (!enemy.isSlowed)
            {
                enemy.isSlowed = true;
                
            }
        }
        base.OnTriggerEnter2D(collision);

    }
}
