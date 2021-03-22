using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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

    protected override  void Update()
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
                StartCoroutine (CanShootCO());
                break;
        }
    }
    
    // Voir Enemy.cs (héritage)
    protected override IEnumerator ShootCO()
    {

        yield return StartCoroutine(intervalleTir());
        isShooting = false;
    }

    protected virtual  IEnumerator intervalleTir()
    { 
        while (n < nbTir && !isArretTemporel)
        {
            isShooting = true;
            yield return StartCoroutine(base.ShootCO());
            yield return new WaitForSeconds(timeIntervale);
            n++;
        }
        n = 0;
    }
}
