using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedShoesModule : PassiveObjects
{
    [SerializeField] private float SpeedMultiplier;
    void Start()
    {
        PlayerMouvement.isSpeedShoesModule = true;
        PlayerMouvement.SpeedMultiplier = SpeedMultiplier;
    }


}
