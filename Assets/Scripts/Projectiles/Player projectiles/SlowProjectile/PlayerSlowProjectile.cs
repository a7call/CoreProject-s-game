using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowProjectile : PlayerProjectiles
{
    [SerializeField] private float slowMultiplier;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.isSlowed)
            {
                enemy.isSlowed = true;
                enemy.aIPath.maxSpeed *= slowMultiplier;


            }
        }
        base.OnTriggerEnter2D(collision);

    }
}
