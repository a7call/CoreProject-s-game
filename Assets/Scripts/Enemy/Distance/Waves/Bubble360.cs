using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

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
                StartCoroutine(CanShoot());
                break;
        }
    }

    // Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
        targetPoint = target;
    }

    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            rb.velocity = Vector2.zero;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
    }

    private void AddShoot()                     
    {
        differentRadius.Insert(0,rayon);
    }
}
