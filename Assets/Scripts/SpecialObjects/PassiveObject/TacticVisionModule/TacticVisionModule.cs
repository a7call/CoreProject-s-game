using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticVisionModule : PassiveObjects
{
    [SerializeField] private float SpeedDiviser = 0f;
    void Start()
    {
        Projectile.isTacticVisionModule = true;
        Projectile.SpeedDiviser = SpeedDiviser;
    }
}
