using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IShootableWeapon 
{
    bool OkToShoot { get; set; }
    void StartShootingProcess();
    bool IsAbleToShoot();
    WeaponScriptableObject DistanceWeaponData { get; }
}
