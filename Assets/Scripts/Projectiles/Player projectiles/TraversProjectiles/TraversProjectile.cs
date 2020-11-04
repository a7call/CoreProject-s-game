using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversProjectile : PlayerProjectiles
{
    private int nbTravers = 0;
    [SerializeField] protected int nbTraversAuthorized;
    public static bool isTraversProjectile = true;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            nbTravers++;
            if (nbTravers >= nbTraversAuthorized) { 
                Destroy(gameObject);
            }
            
        }
        base.OnTriggerEnter2D(collision);

    }
}
