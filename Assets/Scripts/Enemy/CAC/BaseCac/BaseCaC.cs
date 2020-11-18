using System.Collections;
using UnityEngine;
/// <summary>
/// Classe héritière de Cac.cs 
/// Elle contient les même fonctions que Cac.cs
/// </summary>
public class BaseCaC : Cac
{

    
    private void Start()
    {
        currentState = State.Patrolling;
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();
    }


    protected override void Update()
    {
        base.Update();
        switch (currentState) {
        default:
                Debug.LogWarning("not implemented");
                break;
        case State.Patrolling:
                PlayerInSight();
            break;
        case State.Chasing:
                isInRange();
                MoveToPath();
            break;

        case State.Attacking:
                isInRange();
                GetPlayerPos();
                BaseAttack();
                break;
           
        }

    }

  


}
