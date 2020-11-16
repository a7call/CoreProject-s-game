using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiEmeuteModule : PassiveObjects
{
    [SerializeField] private float knockBackForceM = 0f;
  
    void Start()
    {
        CacWeapons.isAntiEmeuteModule = true;
        CacWeapons.knockBackForceMultiplier = knockBackForceM;
    }

   
}
