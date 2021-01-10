using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class HitZoneProjectile : Projectile
{
    [SerializeField] protected GameObject HitZoneGO;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetDirection();
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Instantiate(HitZoneGO, transform.position, Quaternion.identity);
        base.OnTriggerEnter2D(collision);
    }

}

