using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadModule : PassiveObjects
{
    [SerializeField] private float ReloadSpeedMultiplier = 0f;
    void Start()
    {
        DistanceWeapon.isFastReloadModule = true;
        DistanceWeapon.ReloadSpeedMultiplier = ReloadSpeedMultiplier;
    }


}
