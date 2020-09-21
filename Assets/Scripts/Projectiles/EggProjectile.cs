using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectile : Projectile
{
   [SerializeField] protected GameObject mobs;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Instantiate(mobs, transform.position, Quaternion.identity);
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
