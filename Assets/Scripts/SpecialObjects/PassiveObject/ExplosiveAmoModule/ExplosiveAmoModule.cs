using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAmoModule : PassiveObjects
{

    public static float explosiveRadius;
    public static LayerMask hitLayer;
    public static int explosionDamge; 
    [SerializeField] public float explosiveR;
    [SerializeField] public LayerMask hitL;
    [SerializeField] public int explosionD;

    private void Awake()
    {
        explosionDamge = explosionD;
        explosiveRadius = explosiveR;
        hitLayer = hitL;
    }
    private void Start()
    {
        PlayerProjectiles.isExplosiveAmo = true;
        PlayerProjectiles.explosiveRadius = explosiveRadius;
        PlayerProjectiles.hitLayer = hitLayer;
        PlayerProjectiles.explosionDamage = explosionDamge;
    }

    public static void explosionFnc(GameObject gameObject)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, explosiveRadius, hitLayer);
        foreach (Collider2D hit in hits)
        {
            hit.gameObject.GetComponent<Enemy>().TakeDamage(explosionDamge);
        }
    }
}
