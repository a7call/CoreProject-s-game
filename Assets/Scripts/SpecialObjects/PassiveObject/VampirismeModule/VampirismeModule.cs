using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismeModule : PassiveObjects
{
    [SerializeField] private float FullHeartChanceMultiplier = 0f;
    [SerializeField] private float HeartsChanceMultiplier = 0f;
    void Start()
    {
        CacWeapons.isVampirismeModule = true;
        RewardSpawner.isVampirismeModule = true;
        RewardSpawner.VampirismeFullHeartChanceMultiplier = FullHeartChanceMultiplier;
        RewardSpawner.VampirismeHalfHeartChanceMultiplier = HeartsChanceMultiplier;
    }


}
