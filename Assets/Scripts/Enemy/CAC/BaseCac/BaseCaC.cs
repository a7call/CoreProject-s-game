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
        SetData();
        SetMaxHealth();
    }


    protected override void Update()
    {
        base.Update();
        switch (currentState) {

        case State.Patrolling:
            break;
        case State.Chasing:
                isInRange();
            break;

        case State.Attacking:
                isInRange();
                GetPlayerPos();
                StartCoroutine(BaseAttack());
                break;
           
        }

    }

  


}
