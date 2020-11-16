using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class HitZoneProjectile : Projectile
{
    [SerializeField] int nbHit; //durée d'activité de la zone
    [SerializeField] int timeIntervale; //intervalle entre les dégats de zone
    [SerializeField] int zoneRadius; //rayon de la zone de dégats
    [SerializeField] protected GameObject HitZoneGO;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //TakeDamage();
        if (collision.CompareTag("Player")) Instantiate(HitZoneGO, transform.position, Quaternion.identity);
        base.OnTriggerEnter2D(collision);
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

