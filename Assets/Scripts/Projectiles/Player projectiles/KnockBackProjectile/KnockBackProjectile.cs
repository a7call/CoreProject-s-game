using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackProjectile : PlayerProjectiles
{
    [SerializeField] protected float knockBackForce;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.rb.AddForce(dir * knockBackForce* Time.deltaTime);
        }
        base.OnTriggerEnter2D(collision);

    }
}
