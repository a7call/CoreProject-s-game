﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class RafaleDistance : Distance
{
    //[SerializeField] protected RafaleDistanceData RafaleDistanceData;

    protected float timeIntervale;
    protected int nbTir;

    private int n = 0; //compteur pour le while

    protected override void SetData()
    {
        base.SetData();
        timeIntervale = DistanceData.timeIntervale;
        nbTir = DistanceData.nbTir;
    }
    

   

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
            case State.Patrolling:
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                break;
        }

    }


    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        StartCoroutine(intervalleTir());
    }

    protected virtual IEnumerator intervalleTir()
    {
        
        while (n < nbTir && !isArretTemporel)
        {
            base.Shoot();
            yield return new WaitForSeconds(timeIntervale);
            n++;
        }
        n = 0;
    }
}
