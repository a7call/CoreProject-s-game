using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// à retaper entierement 
public class LaserSaber : CacWeapons
{
   
    protected override IEnumerator Attack()
    {
        if(!isAttacking) StartCoroutine(GetComponentInChildren<DeflectAttack>().DeflectProjectils());
        return base.Attack();
       
    }
}
