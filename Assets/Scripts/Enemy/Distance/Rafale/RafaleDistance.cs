using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class RafaleDistance : Distance
{
    [SerializeField] protected RafaleDistanceData RafaleDistanceData;

    private float timeIntervale;
    private int nbTir;

    private int n = 0; //compteur pour le while

    protected override void SetData()
    {
        base.SetData();
        timeIntervale = RafaleDistanceData.timeIntervale;
        nbTir = RafaleDistanceData.nbTir;
    }
    

   

    void Start()
    {
        currentState = State.Patrolling;
        // Set premier targetPoint
        SetFirstPatrolPoint();
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
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                MoveToPath();
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                DontMoveShooting();
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
        
        while (n < nbTir)
        {
            base.Shoot();
            yield return new WaitForSeconds(timeIntervale);
            n++;
        }
        n = 0;
    }
}
