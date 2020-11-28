using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Distance Weapon", menuName = "DistanceWeapon")]

public class DistanceWeaponScriptableObject : ScriptableObject
{
    public float damage;
    public LayerMask enemyLayer;
    public GameObject projectile;
    public float AttackDelay;
    public float Dispersion;
    public int MagSize;
    public float ReloadDelay;
    public Sprite image;
}
