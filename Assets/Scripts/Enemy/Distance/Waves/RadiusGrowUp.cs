using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadiusGrowUp : MonoBehaviour
{
    private float radius;
    private float coeffDir = 1f;
    private float initialRadius = 1f;
    private float time;

    private PlayerHealth playerHealth;

    public RaycastHit2D[] hit;

    [SerializeField] protected LayerMask hitLayer;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        RadiusGrowByTime();
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
        hit = Physics2D.CircleCastAll(transform.position, RadiusGrowByTime(), Vector2.zero, Mathf.Infinity, hitLayer);

        for (int i = 1; i < hit.Length; i++)
        {
            // Si on touche le joueur
            if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit[i].transform.GetComponent<PlayerHealth>().TakeDamage(20);
                print("Vie du joueur si y'a eu un touche " + playerHealth.currentHealth);
            }
            // Si on touche des objets
            else if(hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Destroy(gameObject);
                // On détruit le tir
            }
            // Si on touche les murs => On destroy le tir et on le retire de la liste
            else if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("DestroyableObstacle"))
            {
                // On détruit les obstacles
            }
            i++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusGrowByTime());
    }

}
