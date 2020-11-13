using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoissoneurDeCrystauxModule : PassiveObjects
{
    [SerializeField] private float FullHeartChanceMultiplier;
    [SerializeField] private float HeartsChanceMultiplier;
    void Start()
    {
        RewardSpawner.isMoissoneurDeCrystauxModule = true;
        RewardSpawner.MoissoneurFullHeartChanceMultiplier = FullHeartChanceMultiplier;
        RewardSpawner.MoissoneurHalfHeartChanceMultiplier = HeartsChanceMultiplier;
    }


}
