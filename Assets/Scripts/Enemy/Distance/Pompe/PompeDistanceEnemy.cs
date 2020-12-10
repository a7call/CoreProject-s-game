using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class PompeDistanceEnemy : Distance
{

    
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    private AngleProjectile AngleProjectile;




    void Start()
    {
        GetProjectile();
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
                break;
            case State.Attacking:
                isInRange();
                DontMoveShooting();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                break;
        }

    }
    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = - decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for(int i=0; i <projectiles.Length; i++)
            {
                AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
                GameObject myProjectile = GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
                myProjectile.transform.parent = gameObject.transform;
        }

    }


    private void GetProjectile()
    {
        foreach(GameObject projectile in projectiles)
        {
           AngleProjectile = projectile.GetComponent<AngleProjectile>();
        }
    }
}
