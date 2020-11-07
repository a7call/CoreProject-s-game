using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] int nbHit;
    [SerializeField] float timeIntervale;
    [SerializeField] float zoneRadius;
    private int n = 0;
    private PlayerHealth playerHealth;
    //[SerializeField] protected GameObject HitZoneGO;

    private void Awake()
    {
        StartCoroutine(hitZone());
        playerHealth = FindObjectOfType<PlayerHealth>();
    }


    protected virtual IEnumerator hitZone()
    {
        
        while (n < nbHit)
        {
            //OnDrawGizmos();
            yield return new WaitForSeconds(timeIntervale);

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, zoneRadius);
           

            foreach (Collider2D h in hits)
            {
                if (h.CompareTag("Player"))
                {
                    playerHealth.TakeDamage(20);
                }
               

            }
            n++;
        }

        Destroy(gameObject);

    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,zoneRadius);
    }
}
