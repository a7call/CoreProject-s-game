using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
   
    private bool isActive;
    [SerializeField] protected float ZoneRadius;
    [SerializeField] protected int ZoneDamage;
    [SerializeField] protected float hitTimer;
    [SerializeField] protected float zoneTimer;
    // Start is called before the first frame update
    protected  void Start()
    {
        //base.Start();
        StartCoroutine(ActiveZone());
        StartCoroutine(ZoneCo());
    }

    protected virtual IEnumerator ZoneCo()
    {
        while (isActive)
        {
            
            Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, ZoneRadius);

            foreach (Collider2D hit in collision)
            {
                if (hit.CompareTag("Player"))
                {
                    Player playerHealthScript = hit.GetComponent<Player>();
                    playerHealthScript.TakeDamage(ZoneDamage);
                }

            }

            yield return new WaitForSeconds(hitTimer);
        }

    }

   

    private IEnumerator ActiveZone()
    {
        isActive = true;
        yield return new WaitForSeconds(zoneTimer);
        isActive = false;
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ZoneRadius);
    }
}



