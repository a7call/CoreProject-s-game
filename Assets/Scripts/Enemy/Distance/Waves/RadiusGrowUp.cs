using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadiusGrowUp : MonoBehaviour
{
    protected float radius;
    protected float coeffDir = 1f;
    protected float initialRadius = 0.2f;
    protected float time;

    private PlayerHealth playerHealth;

    protected RaycastHit2D[] hits;

    [SerializeField] protected LayerMask hitLayer;

    protected virtual void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RadiusGrowByTime();
        ShootRadius();
    }

    protected virtual float Timer()
    {
        time += Time.deltaTime;
        return time;
    }

    protected virtual float RadiusGrowByTime()
    {
        radius = initialRadius + coeffDir * Timer();
        return radius;
    }

    protected virtual void ShootRadius()
    {
        hits = Physics2D.CircleCastAll(transform.position, RadiusGrowByTime(), Vector2.zero, Mathf.Infinity, hitLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) >= Mathf.Abs(RadiusGrowByTime() - 0.01f) && Vector3.Distance(transform.position, hit.transform.position) <= Mathf.Abs(RadiusGrowByTime() + 0.01f))
                {
                    hit.transform.GetComponent<PlayerHealth>().TakeDamage(20);
                    print("Vie du joueur si y'a eu un touche " + playerHealth.currentHealth);
                }
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Destroy(gameObject);
            }

            // Détruire les objets qui sont destructibles
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusGrowByTime());
    }

}
