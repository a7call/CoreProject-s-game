using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoleurDeTombe : PassiveObjects
{
    [SerializeField] private float KeyChanceMultiplier = 0f;
    [SerializeField] private float CoinChanceMultiplier = 0f;
    void Start()
    {
        RewardSpawner.isVoleurDeTombeModule = true;
        RewardSpawner.VoleurKeyChanceMultiplier = KeyChanceMultiplier;
        RewardSpawner.VoleurCoinChanceMultiplier = CoinChanceMultiplier;
    }


}
