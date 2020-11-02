using System.Collections;
using UnityEngine;

public class NuclearExplosionModule : PassiveObjects
{
    [SerializeField] private int explosionDamageMultiplier;
    [SerializeField] private float nuclearDotTimer;
    [SerializeField] private int nuclearDotDamage;
   
    private void Start()
    {
        ExplosionProjectile.isNuclearExplosionModule = true;
        ExplosionProjectile.explosionDamageMultiplier = explosionDamageMultiplier;
        PlayerProjectiles.dotTimeBetweenHits = nuclearDotTimer;
        PlayerProjectiles.dotDamage = nuclearDotDamage;
    }
}
