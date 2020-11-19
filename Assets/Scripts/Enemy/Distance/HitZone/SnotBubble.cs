using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotBubble : Distance
{
    //[SerializeField] protected GameObject HitZoneGO;


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
            
        }

    }
 
    
}

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
   // protected override void Aggro()
    //{
      //  targetPoint = target;
        //GameObject.Instantiate(HitZoneGO, transform.position, Quaternion.identity);

    //}

