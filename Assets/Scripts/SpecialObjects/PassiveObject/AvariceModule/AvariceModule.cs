using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvariceModule : PassiveObjects
{
    [SerializeField] private float ChanceMultiplier = 0f;
    void Start()
    {
        RewardSpawner.isAvariceModule = true;
        RewardSpawner.AvariceCoinChanceMultiplier = ChanceMultiplier;
    }


}
