using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapproModule : PassiveObjects
{
    [SerializeField] private float AmmoChanceMultiplier;
    void Start()
    {
        RewardSpawner.isReapproModule = true;
        RewardSpawner.ReapproAmmoChanceMultiplier = AmmoChanceMultiplier;
    }


}
