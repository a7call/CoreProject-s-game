using System.Collections;
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

    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
    }
    protected override void SetData()
    {
        base.SetData();
        timeIntervale = DistanceData.timeIntervale;
        nbTir = DistanceData.nbTir;
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
                StartCoroutine("CanShoot");
                break;
        }
        ShouldNotMoveDuringShooting();
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
            isShooting = true;
            base.Shoot();
            yield return new WaitForSeconds(timeIntervale);
            n++;
        }
        n = 0;
    }
}
