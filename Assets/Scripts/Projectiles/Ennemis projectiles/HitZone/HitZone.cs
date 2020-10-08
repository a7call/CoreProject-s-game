using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] int nbHit;
    [SerializeField] int timeIntervale;
    [SerializeField] float zoneRadius;
    private int n = 0;

    //[SerializeField] protected GameObject HitZoneGO;

    private void Awake()
    {
        StartCoroutine(hitZone());
    }


    protected virtual IEnumerator hitZone()
    {

        while (n < nbHit)
        {
            yield return new WaitForSeconds(timeIntervale);

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, zoneRadius);

            foreach (Collider2D h in hits)
            {
                if (h.CompareTag("Player"))
                {
                    print("test");
                }
                // TakeDamage();

            }
            n++;
        }

        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

}
