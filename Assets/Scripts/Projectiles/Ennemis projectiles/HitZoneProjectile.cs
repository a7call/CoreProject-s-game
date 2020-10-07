using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class HitZoneProjectile : Projectile
{
     private int n = 0; //Compteur pour la durée des degats zone
    [SerializeField] int hitTime = 3; //durée d'activité de la zone
    [SerializeField] int timeIntervale = 1; //intervalle entre les degats de zone
    [SerializeField] int zoneRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Lauch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TakeDamage();
        HitZone();
    }

    protected override void GetDirection()
    {
        base.GetDirection();
    }

    protected override void Lauch()
    {
        base.Lauch();
    }

    protected virtual void HitZone()
    {
        while (n < (hitTime - 1))
        {
            StartCoroutine(WaitingTime());
            base.speed = 0;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, zoneRadius);

            foreach (Collider2D h in hits)
            {
                // TakeDamage();
            }
            n++;
        }

        Destroy(gameObject);
        
    }
    private IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(timeIntervale);
    }


}

