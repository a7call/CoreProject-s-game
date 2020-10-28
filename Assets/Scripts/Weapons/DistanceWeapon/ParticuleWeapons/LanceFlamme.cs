using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceFlamme : DistanceWeapon
{
    protected override void Update()
    {
        GetAttackDirection();
    }
    protected override void SetData()
    {
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
    }
}
