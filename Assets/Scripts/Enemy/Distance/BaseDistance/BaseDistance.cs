using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class BaseDistance : Distance
{
    protected override void Awake()
    {
        // Set data
        base.Awake();
        SetData();
        SetMaxHealth();
    }

    protected override async void Update()
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
