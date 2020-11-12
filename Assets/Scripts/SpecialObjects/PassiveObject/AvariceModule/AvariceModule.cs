using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvariceModule : PassiveObjects
{
    [SerializeField] private float ChanceMultiplier;
    void Start()
    {
        RewardSpawner.isAvariceModule = true;
        RewardSpawner.AvariceCoinChanceMultiplier = ChanceMultiplier;
    }


}
