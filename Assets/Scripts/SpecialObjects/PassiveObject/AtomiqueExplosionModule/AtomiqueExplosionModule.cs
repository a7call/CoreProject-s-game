using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomiqueExplosionModule : PassiveObjects
{
    [SerializeField] private int explosionDamageMultiplier;
    void Start()
    {
        ExplosionProjectile.isAtomiqueExplosionModule = true;
        ExplosionProjectile.explosionDamageMultiplier = explosionDamageMultiplier;
    }

}
