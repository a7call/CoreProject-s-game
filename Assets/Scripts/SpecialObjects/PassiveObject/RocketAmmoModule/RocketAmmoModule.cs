using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAmmoModule : PassiveObjects
{
    [SerializeField] private float SpeedMultiplier = 0f;
    void Start()
    {
        PlayerProjectiles.isRocketAmmoModule = true;
        PlayerProjectiles.SpeedMultiplier = SpeedMultiplier;
    }


}
