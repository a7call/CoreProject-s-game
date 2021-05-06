using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IShootableWeapon : IPlayerWeapon
{
    bool OkToShoot { get; set; }
    void StartShootingProcess();
    bool IsAbleToShoot();
   
}
