
using UnityEngine;

public class ChercheurDeTresorModule : PassiveObjects
{
    [SerializeField] private float CoffreChanceMultiplier = 0f;
    [SerializeField] private float KeyChanceMultiplier = 0f;
    void Start()
    {
        RewardSpawner.isChercheurDeTresorModule = true;
        RewardSpawner.TresorKeyChanceMultiplier = KeyChanceMultiplier;
        RewardSpawner.TresorCoffreChanceMultiplier = CoffreChanceMultiplier;
    }


}
