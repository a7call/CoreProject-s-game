using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadModule : PassiveObjects
{
    [SerializeField] private float ReloadSpeedMultiplier;
    void Start()
    {
        DistanceWeapon.isFastReloadModule = true;
        DistanceWeapon.ReloadSpeedMultiplier = ReloadSpeedMultiplier;
    }


}
