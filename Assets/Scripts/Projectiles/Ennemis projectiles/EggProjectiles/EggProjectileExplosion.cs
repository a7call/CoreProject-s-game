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

    protected void Update()
    {
        Lauch();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] playerHit =  Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
        foreach (Collider2D hit in playerHit)
        {
            hit.GetComponent<PlayerHealth>().TakeDamage(explosionDamage);
        }
        Destroy(gameObject);
    }

    protected override void GetDirection()
    {
        base.GetDirection();
    }

    protected override void Lauch()
    {
        base.Lauch();
    }

}
