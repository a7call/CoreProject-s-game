using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de distance 
/// Contient en plus de classe distance une coroutine de projectile (spécial) => voir EggProjectile.cs
/// </summary>
public class TentaculeAstronaute : Distance
{

    void Start()
    {
        // Set data
        SetData();
        SetMaxHealth();
        
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer

                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine(CanShootCO());
                break;
        }
    }



}
