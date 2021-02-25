using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaculeAstronauteSpawner : Distance
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
                StartCoroutine("CanShoot");
                break;
        }
    }



    // Projectile spé
     [SerializeField] protected GameObject EggsSpawner;
    // Check si attaque spé rdy
    private bool isSpeRdy = true;
    // Time entre deux attaque spé
     [SerializeField] protected float reloadSpe;

    // Couroutine du shoot
    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot && !isSpeRdy)
        {
            // Ne peut plus tirer car déjà entrain de tirer
            isReadytoShoot = false;
            // Tire
            Shoot();
            // Repos entre deux tires
            yield return new WaitForSeconds(restTime);
            // Peut tirer de nouveau
            isReadytoShoot = true;
        }

          else if (isSpeRdy && isShooting && isReadytoShoot)
          {
              // Ne peut plus tirer car déjà entrain de tirer spé + normal
              isSpeRdy = false;
              isReadytoShoot = false;
              // Shoot spé
              Eggs();
              // Repos entre deux tire
              yield return new WaitForSeconds(restTime);
              // Peut tirer normalement
              isReadytoShoot = true;
              // Reload attaque spé
              yield return new WaitForSeconds(reloadSpe);
              // attaque spé rdy
              isSpeRdy = true;
          }
        
    }

    // Instantiate projectile spé
    protected void Eggs()
    {
        GameObject.Instantiate(EggsSpawner, transform.position, Quaternion.identity);
    }
    


}
