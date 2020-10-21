using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///Script de l'attaque avec un zone d'impact (cercle)
/// </summary>
public class Masse : CacWeapons
{
   
    private void Update()
    {
        GetAttackDirection();
    }
   
    protected override void GetAttackDirection()
    {
        base.GetAttackDirection();
    }
    protected override void AttackCACZone()
    {
        base.AttackCACZone();
    }

}
