using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Distance Weapon", menuName = "DistanceWeapon")]

public class DistanceWeaponScriptableObject : WeaponScriptableObject
{
   
    public GameObject projectile;
    public float Dispersion;
    public int MagSize;
    public float ReloadDelay;
    public int AmmoStock;
   
}
