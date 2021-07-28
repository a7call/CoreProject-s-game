using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de distance 
/// Contient en plus de classe distance une coroutine de projectile (spécial) => voir EggProjectile.cs
/// </summary>
public class TentaculeAstronaute : Distance
{

    protected override void Start()
    {
        // Set data
        SetData();
        SetMaxHealth();
        
    }
    protected override void Update()
    {

    }



}
