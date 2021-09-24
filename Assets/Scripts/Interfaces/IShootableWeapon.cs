using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootableWeapon : IPlayerWeapon
{
    void StartShootingProcess(int shotValue);
    bool IsAbleToShoot();

    void toReload();   
}
