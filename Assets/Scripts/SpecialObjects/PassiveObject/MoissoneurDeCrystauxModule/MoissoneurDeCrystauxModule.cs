using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoissoneurDeCrystauxModule : PassiveObjects
{
    [SerializeField] private float FullHeartChanceMultiplier = 0f;
    [SerializeField] private float HeartsChanceMultiplier = 0f;
    void Start()
    {
        RewardSpawner.isMoissoneurDeCrystauxModule = true;
        RewardSpawner.MoissoneurFullHeartChanceMultiplier = FullHeartChanceMultiplier;
        RewardSpawner.MoissoneurHalfHeartChanceMultiplier = HeartsChanceMultiplier;
    }


}
