﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomiqueExplosionModule : PassiveObjects
{
    [SerializeField] private float explosionDamageMultiplier = 0;
    void Start()
    {
        PlayerProjectiles.isAtomiqueExplosionModule = true;
        PlayerProjectiles.explosionDamageMultiplier = explosionDamageMultiplier;
    }

}
