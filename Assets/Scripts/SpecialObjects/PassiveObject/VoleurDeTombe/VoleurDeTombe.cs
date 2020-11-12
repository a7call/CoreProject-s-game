using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoleurDeTombe : PassiveObjects
{
    [SerializeField] private float KeyChanceMultiplier;
    [SerializeField] private float CoinChanceMultiplier;
    void Start()
    {
        RewardSpawner.isVoleurDeTombeModule = true;
        RewardSpawner.VoleurKeyChanceMultiplier = KeyChanceMultiplier;
        RewardSpawner.VoleurCoinChanceMultiplier = CoinChanceMultiplier;
    }


}
