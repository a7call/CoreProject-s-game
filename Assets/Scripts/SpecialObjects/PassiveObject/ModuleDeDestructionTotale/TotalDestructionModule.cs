using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalDestructionModule : PassiveObjects
{
   [SerializeField] private int damageMultiplier = 0;
    void Start()
    {
        Weapons.isTotalDestructionModule = true;
        Weapons.damageMultiplier = damageMultiplier;
    }

   
}
