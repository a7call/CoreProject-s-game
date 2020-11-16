using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryModule : PassiveObjects
{
    [SerializeField] private int CadenceMultiplier = 0;
    void Start()
    {
        CacWeapons.isFuryModule = true;
        CacWeapons.CadenceMultiplier = CadenceMultiplier;
    }


}