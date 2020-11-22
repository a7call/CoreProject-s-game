using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmoCase : Rewards
{
    [SerializeField] private int numberOfAmoInCase = 0;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInChildren<WeaponManager>().GetComponentInChildren<DistanceWeapon>())
                {
                DistanceWeapon weapon = collision.GetComponentInChildren<WeaponManager>().GetComponentInChildren<DistanceWeapon>();
                weapon.AmmoStock += numberOfAmoInCase;

                 }
            if (collision.GetComponentInChildren<WeaponManager>().GetComponentInChildren<CacWeapons>())
                 {
                return;
                 }
            base.OnTriggerEnter2D(collision);
        }
    }
          
}
