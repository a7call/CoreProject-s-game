using UnityEngine;

public class CanonRapideModule : PassiveObjects
{
    [SerializeField] private int CadenceMultiplier = 0;
    void Start()
    {
        DistanceWeapon.isCanonRapideModule = true;
        DistanceWeapon.CadenceMultiplier = CadenceMultiplier;
    }


}