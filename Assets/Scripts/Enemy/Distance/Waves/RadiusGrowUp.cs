using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadiusGrowUp : MonoBehaviour
{
    private float radius;
    private float coeffDir = 1f;
    private float initialRadius = 0.2f;
    private float time;

    private PlayerHealth playerHealth;

    public RaycastHit2D[] hits;

    [SerializeField] protected LayerMask hitLayer;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        RadiusGrowByTime();
        ShootRadius();
    }

    private float Timer()
    {
        time += Time.deltaTime;
        return time;
    }

    private float RadiusGrowByTime()
    {
        radius = initialRadius + coeffDir * Timer();
        return radius;
    }

    public void ShootRadius()
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusGrowByTime());
    }

}
