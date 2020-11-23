using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectileExplosion : Projectile
{
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected int explosionDamage;

    protected void Start()
    {
        GetDirection();
        // Invoke("PopMobs", 3f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       
            Collider2D[] playerHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
            foreach (Collider2D hit in playerHit)
            {
                hit.gameObject.GetComponent<PlayerHealth>().TakeDamage(explosionDamage);
            }
             base.OnTriggerEnter2D(collision);
    }

}
