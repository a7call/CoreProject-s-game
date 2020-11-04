using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomiqueExplosionModule : PassiveObjects
{
    [SerializeField] private int explosionDamageMultiplier;
    void Start()
    {
        PlayerProjectiles.isAtomiqueExplosionModule = true;
        PlayerProjectiles.explosionDamageMultiplier = explosionDamageMultiplier;
    }

}
