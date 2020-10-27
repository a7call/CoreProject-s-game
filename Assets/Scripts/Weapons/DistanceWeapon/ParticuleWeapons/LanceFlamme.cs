using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceFlamme : DistanceWeapon
{
    protected override void Update()
    {
        GetAttackDirection();
    }

    protected void ActivateShoot() {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
