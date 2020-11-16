using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpRangeCacModule : PassiveObjects
{
    [SerializeField] private float RangeMultiplier = 0f;
    void Start()
    {
        CacWeapons.isUpRangeCacModule = true;
        CacWeapons.RangeMultiplier = RangeMultiplier;
    }


} 
