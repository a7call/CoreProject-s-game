using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : Projectile
{
    [SerializeField] int nbHit = 0;
    [SerializeField] float timeIntervale = 0f;
    [SerializeField] float zoneRadius = 0f;
    private int n = 0;
    
    //[SerializeField] protected GameObject HitZoneGO;

    protected override void Awake()
    {
        StartCoroutine(hitZone());
    }

    protected override void Update()
    {
        
    }

    protected virtual IEnumerator hitZone()
    {
        
        while (n < nbHit)
        {
            //OnDrawGizmos();
            

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, zoneRadius);
           

            foreach (Collider2D h in hits)
            {
                if (h.CompareTag("Player"))
                {
                    h.GetComponent<PlayerHealth>().TakeDamage(1);
                }
               

            }
            n++;
            yield return new WaitForSeconds(timeIntervale);
        }

        Destroy(gameObject);

    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,zoneRadius);
    }
}
