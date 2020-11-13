using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismeModule : PassiveObjects
{
    [SerializeField] private float FullHeartChanceMultiplier;
    [SerializeField] private float HeartsChanceMultiplier;
    void Start()
    {
        CacWeapons.isVampirismeModule = true;
        RewardSpawner.isVampirismeModule = true;
        RewardSpawner.VampirismeFullHeartChanceMultiplier = FullHeartChanceMultiplier;
        RewardSpawner.VampirismeHalfHeartChanceMultiplier = HeartsChanceMultiplier;
    }


}
