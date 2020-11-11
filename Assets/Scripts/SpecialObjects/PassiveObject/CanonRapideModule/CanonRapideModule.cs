using UnityEngine;

public class CanonRapideModule : PassiveObjects
{
    [SerializeField] private int CadenceMultiplier;
    void Start()
    {
        DistanceWeapon.isCanonRapideModule = true;
        DistanceWeapon.CadenceMultiplier = CadenceMultiplier;
    }


}