using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionModule : PassiveObjects
{
    [SerializeField] private int PrecisionMultiplier;
    void Start()
    {
        DistanceWeapon.isPrecisionModule = true;
        DistanceWeapon.PrecisionMultiplier = PrecisionMultiplier;
    }


}
