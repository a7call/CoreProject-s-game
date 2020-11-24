using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaserSniper : RafaleDistance
{
    

    
    [SerializeField] protected float delayMovement;


    protected override void Shoot()
    {
        base.Shoot();
        StartCoroutine(MovementDelay());
    }

   
    protected IEnumerator MovementDelay()
    {
        rb.velocity = Vector2.zero;
        currentState = State.ShootingLaser;
        yield return new WaitForSeconds(delayMovement);
        currentState = State.Chasing;

    }


  
}
