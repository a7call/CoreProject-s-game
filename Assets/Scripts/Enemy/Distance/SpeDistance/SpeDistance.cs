using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeDistance : Distance
{

    void Start()
    {
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        // Vie initial
        SetMaxHealth();
    }
    private void Update()
    {
        // récupération de l'aggro
        Aggro();
        // script de patrol
        Patrol();
        // suit le path créé et s'arrête pour tirer
        if(!isShooting ) MoveToPath();
        // Couroutine gérant les shoots 
        StartCoroutine("CanShoot");
        // Couroutine gérant les shoots spé
       
    }


    protected override void SetData()
    {
        base.SetData();
    }

    //Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {


        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            // Stop patrouiller
            isPatroling = false;
            // Target = player
            targetPoint = target;
            // Stop to shoot
            rb.velocity = Vector2.zero;
            // Entrain de shoot
            isShooting = true;
        }
        else
        {
            // S'arrete de tirer popur suivre le joueur
            isShooting = false;

        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
    }

    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }



    //Health



    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }


    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }




    //Attack


    // Projectile spé
    [SerializeField] protected GameObject EggsProjectiles;
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


    // Voir Distance.cs (héritage)
    protected override void Shoot()
    {
        base.Shoot();
    }
    // Instantiate projectile spé
    protected void Eggs()
    {
        GameObject.Instantiate(EggsProjectiles, transform.position, Quaternion.identity);
    }


}
