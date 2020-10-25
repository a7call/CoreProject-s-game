using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    private PlayerHealth playerHealth;
    private RadiusGrowUp radiusGrowUp;

    [SerializeField] private bool damageDone = false;
    //isReadyToShoot initalisée à true
    //isShooting non initialisé

    [SerializeField] private int numberOfShoot;
    public int currentIndex = 0;
    private int longueurListe;
    [SerializeField] private List<RadiusGrowUp> differentRadius = new List<RadiusGrowUp>();

    private bool tryAugmenteListe = true;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        radiusGrowUp = FindObjectOfType<RadiusGrowUp>();

        longueurListe = differentRadius.Count;

        currentState = State.Chasing;
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
            case State.Chasing:
                Aggro();
                isInRange();
                MoveToPath();
                break;
            case State.Attacking:
                isInRange();
                //StartCoroutine("CanShoot()");
                if(tryAugmenteListe == true) AddShoot();
                break;
        }

    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
        targetPoint = target;
    }

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
    }

    protected override void isInRange()
    {
        base.isInRange();
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


    // Health


    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

 

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Attack

    // Voir Enemy.cs (héritage)
    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot)
        {
            isReadytoShoot = false;
            rb.velocity = Vector2.zero;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
            print(isReadytoShoot);
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        if (currentState == State.Attacking)
        {
            // On instancie les variables
            isShooting = true;
            isReadytoShoot = false;
            // On empêche l'ennemi de bouger car il tire
            rb.velocity = Vector2.zero;
            //print("L'ennemi s'arrete");
            AddShoot();
            //// On augemente la taille de la liste pour rajouter un tir

            //for (int i = 1; i < longueurListe; i++)
            //{
            //longueurListe++;
            //currentIndex++;
            //print("On aggrandit la liste pour stocker le rayon, la taille de la liste est de     " +longueurListe);
            //// On place le rayon dans la liste
            //differentRadius[currentIndex] = gameObject.GetComponent<RadiusGrowUp>();
            //differentRadius[currentIndex].ShootRadius();
            //print("Le tir s'est effectué");

            //}
        }
    }

    private void AddShoot()
    {
        tryAugmenteListe = false;
        longueurListe++;
        currentIndex++;
        differentRadius[currentIndex] = radiusGrowUp;
        //differentRadius[currentIndex].ShootRadius();

    }

    //private void RemoveShoot()
    //{
    //    if(radiusGrowUp.hit[].transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //    longueurListe--;
    //    currentIndex--;
    //    Destroy(differentRadius[currentIndex]);
    //    }
    //}
}
