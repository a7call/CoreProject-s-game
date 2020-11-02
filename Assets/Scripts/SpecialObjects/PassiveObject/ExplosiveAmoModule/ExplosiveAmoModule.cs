using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAmoModule : PassiveObjects
{

    [SerializeField] private float explosiveRadius;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private int explosionDamge;

    private void Start()
    {
        PlayerProjectiles.isExplosiveAmo = true;
        PlayerProjectiles.explosiveRadius = explosiveRadius;
        PlayerProjectiles.hitLayer = hitLayer;
        PlayerProjectiles.explosionDamage = explosionDamge;
    }
}
